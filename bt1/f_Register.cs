using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.RegularExpressions;

namespace bt1
{
    public partial class f_Register : Form
    {
        private int position;

        public f_Register()
        {
            InitializeComponent();
        }

        public f_Register(int pos)
        {
            InitializeComponent();
            this.position = pos;
        }

        private void bt_Register_Click(object sender, EventArgs e)
        {
            // 1. Kiểm tra điền trống dữ liệu trước
            if (!verif())
            {
                MessageBox.Show("Vui lòng điền đầy đủ thông tin và chọn ảnh!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // 2. Kiểm tra độ mạnh mật khẩu bằng Regex [Nâng cao]
            if (!IsStrongPassword(txb_Pass.Text))
            {
                MessageBox.Show("Mật khẩu phải chứa ít nhất 8 ký tự, bao gồm ít nhất 1 chữ hoa, 1 số và 1 ký tự đặc biệt!", "Mật khẩu yếu", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txb_Pass.Focus();
                return;
            }

            // 3. Kiểm tra tính duy nhất của Username
            if (!existUser())
            {
                MessageBox.Show("Tên Tài Khoản Đã Tồn Tại!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txb_User.Focus();
                return;
            }

            // 4. Kiểm tra tính duy nhất của Email
            if (!existEmail())
            {
                MessageBox.Show("Email Đã Tồn Tại!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txb_Email.Focus();
                return;
            }

            // 5. Nếu mọi điều kiện thỏa mãn -> Tiến hành gửi OTP
            string generatedOTP = sendOTP(txb_Email.Text);

            if (generatedOTP != null)
            {
                f_OTP otpForm = new f_OTP(generatedOTP, txb_Email.Text);
                this.Hide(); // Ẩn Form đăng ký đi

                if (otpForm.ShowDialog() == DialogResult.OK) // Người dùng nhập đúng OTP
                {
                    if (Register())
                    {
                        MessageBox.Show("Đăng Ký Thành Công. Vui lòng chờ Admin duyệt tài khoản!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        // Gỡ bỏ sự kiện hỏi thoát Form để đóng an toàn
                        this.FormClosing -= f_Register_FormClosing;
                        this.Close(); // Đóng hoàn toàn Form đăng ký thành công

                        return; // THÊM LỆNH NÀY: Kết thúc hàm ngay lập tức, bỏ qua hoàn toàn lệnh this.Show() ở dưới!
                    }
                    else
                    {
                        MessageBox.Show("Lỗi trong quá trình thao tác Database!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        this.Show(); // Nếu lỗi database thì hiện lại Form đăng ký để chỉnh sửa thông tin
                    }
                }
                else
                {
                    // Người dùng bấm Hủy bỏ OTP hoặc tắt Form OTP nửa chừng
                    this.Show(); // Hiện lại Form đăng ký cho người dùng
                }
            }
        }

        private void ptb_Picture_Click(object sender, EventArgs e)
        {
            OpenFileDialog opnfd = new OpenFileDialog();
            opnfd.Filter = "Image Files (*.jpg;*.jpeg;*.png)|*.jpg;*.jpeg;*.png";
            if (opnfd.ShowDialog() == DialogResult.OK)
            {
                ptb_Picture.Image = new Bitmap(opnfd.FileName);
                ptb_Picture.SizeMode = PictureBoxSizeMode.Zoom;
            }
        }

        bool Register()
        {
            My_DB my_db = new My_DB();
            MemoryStream picture = new MemoryStream();
            ptb_Picture.Image.Save(picture, ptb_Picture.Image.RawFormat);

            SqlCommand command;
            // Tùy theo bảng lựa chọn để ánh xạ chuẩn cấu trúc dữ liệu của bạn
            if (!rbt_Role.Checked)
                command = new SqlCommand(
                    "INSERT INTO HR (MSGV, Fname, Lname, Position, Username, Password, Email, Pic, Valid) " +
                    "VALUES (@id, @fn, @ln, @pos, @user, @pass, @email, @pic, @val)", my_db.getConnection);
            else
                command = new SqlCommand(
                    "INSERT INTO Login (Id, Fname, Lname, Position, Username, Password, Email, Pic, Valid) " + // Sửa ID thành Id cho đồng bộ bảng Login
                    "VALUES (@id, @fn, @ln, @pos, @user, @pass, @email, @pic, @val)", my_db.getConnection);

            command.Parameters.Add("@id", SqlDbType.Int).Value = int.Parse(txb_ID.Text);
            command.Parameters.Add("@fn", SqlDbType.NVarChar).Value = txb_Fname.Text;
            command.Parameters.Add("@ln", SqlDbType.NVarChar).Value = txb_Lname.Text;
            command.Parameters.Add("@pos", SqlDbType.Int).Value = position;
            command.Parameters.Add("@user", SqlDbType.VarChar).Value = txb_User.Text;
            command.Parameters.Add("@pass", SqlDbType.VarChar).Value = txb_Pass.Text;
            command.Parameters.Add("@email", SqlDbType.VarChar).Value = txb_Email.Text;
            command.Parameters.Add("@pic", SqlDbType.Image).Value = picture.ToArray();
            command.Parameters.Add("@val", SqlDbType.Int).Value = 0; // VALID = 0: Chờ duyệt [cite: 6]

            my_db.openConnection();
            bool result = command.ExecuteNonQuery() == 1;
            my_db.closeConnection();
            return result;
        }

        // Tối ưu hóa truy vấn bằng Subquery để loại bỏ Cross Join làm chậm DB
        bool existUser()
        {
            My_DB my_db = new My_DB();
            SqlCommand cmd = new SqlCommand(
                "SELECT (SELECT Count(*) FROM Login WHERE Username = @user) + (SELECT Count(*) FROM HR WHERE Username = @user)",
                my_db.getConnection);
            cmd.Parameters.Add("@user", SqlDbType.NVarChar).Value = txb_User.Text.Trim();

            my_db.openConnection();
            bool exists = (int)cmd.ExecuteScalar() > 0;
            my_db.closeConnection();
            return !exists;
        }

        bool existEmail()
        {
            My_DB my_db = new My_DB();
            SqlCommand cmd = new SqlCommand(
                "SELECT (SELECT Count(*) FROM Login WHERE Email = @email) + (SELECT Count(*) FROM HR WHERE Email = @email)",
                my_db.getConnection);
            cmd.Parameters.Add("@email", SqlDbType.NVarChar).Value = txb_Email.Text.Trim();

            my_db.openConnection();
            bool exists = (int)cmd.ExecuteScalar() > 0;
            my_db.closeConnection();
            return !exists;
        }

        bool verif()
        {
            return !(string.IsNullOrWhiteSpace(txb_ID.Text) || string.IsNullOrWhiteSpace(txb_Fname.Text) ||
                     string.IsNullOrWhiteSpace(txb_Lname.Text) || string.IsNullOrWhiteSpace(txb_User.Text) ||
                     string.IsNullOrWhiteSpace(txb_Pass.Text) || string.IsNullOrWhiteSpace(txb_Email.Text) ||
                     ptb_Picture.Image == null);
        }

        // Đổi phạm vi thành public/internal để Form OTP có thể gọi lại khi nhấn "Gửi lại"
        public string sendOTP(string targetEmail)
        {
            Random rand = new Random();
            string otpCode = rand.Next(100000, 999999).ToString();

            try
            {
                var fromAddress = new MailAddress("duongduyvinh206@gmail.com", "Hệ Thống Quản Lý");
                var toAddress = new MailAddress(targetEmail);
                string fromPassword = "bsxxalktelewxuwo";

                string subject = "Mã xác thực đăng ký tài khoản";
                string body = $"Mã OTP xác thực của bạn là: {otpCode}\nLưu ý: Mã này chỉ có hiệu lực trong vòng 5 phút.";

                var smtp = new SmtpClient
                {
                    Host = "smtp.gmail.com",
                    Port = 587,
                    EnableSsl = true,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential(fromAddress.Address, fromPassword)
                };

                using (var message = new MailMessage(fromAddress, toAddress) { Subject = subject, Body = body })
                {
                    smtp.Send(message);
                }
                return otpCode;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi gửi Email: " + ex.Message, "Lỗi mạng");
                return null;
            }
        }

        private void f_Register_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn thoát đăng ký không?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.No)
            {
                e.Cancel = true;
            }
            else
            {
                this.FormClosing -= f_Register_FormClosing;
                Application.Exit();
            }
        }

        private void f_Register_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (ptb_Picture.Image != null)
            {
                ptb_Picture.Image.Dispose();
            }
        }

        bool IsStrongPassword(string password)
        {
            string pattern = @"^(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$"; 
            return Regex.IsMatch(password, pattern);
        }
    }
}