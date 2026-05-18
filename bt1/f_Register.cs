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
using static System.Net.WebRequestMethods;

namespace bt1
{
    public partial class f_Register : Form
    {
        public f_Register()
        {
            InitializeComponent();
        }
        // 1. Khai báo biến lưu vị trí (1 = Student, 2 = HR)
        private int position;

        // 2. Sửa hàm khởi tạo để nhận tham số từ f_Login truyền sang
        public f_Register(int pos)
        {
            InitializeComponent();
            this.position = pos;
        }

       
        private void bt_Register_Click(object sender, EventArgs e)
        {
            if (existUser())         
            {
                if (existEmail())    
                {
                    if (verif())    
                    {
                        string generatedOTP = sendOTP(txb_Email.Text);

                        if (generatedOTP != null)
                        {
                            f_OTP otp = new f_OTP(generatedOTP);
                            //otp.receivedOTP = txb_Email.Text;
                            this.Hide();
                            if (otp.ShowDialog() == DialogResult.OK)  // OTP đúng
                            {
                                if (Register())
                                {
                                    MessageBox.Show("Đăng Ký Thành Công.", "Thông báo");
                                }

                                else
                                    MessageBox.Show("Lỗi trong quá trình thao tác Database", "Lỗi");
                            }
                            this.Show();
                        }
                    }
                    else
                    {
                        MessageBox.Show("Vui lòng điền đầy đủ thông tin và chọn ảnh!", "Cảnh báo");
                    }
                }
                else
                    MessageBox.Show("Email Đã Tồn Tại!");
            }
            else
                MessageBox.Show("Tên Tài Khoản Đã Tồn Tại!");
        }

        // Hàm xử lý chọn ảnh (Bạn cần có nút btn_ChooseImage)
        private void ptb_Picture_Click(object sender, EventArgs e)
        {
            OpenFileDialog opnfd = new OpenFileDialog();
            opnfd.Filter = "Image Files (*.jpg;*.jpeg;.*.gif;*.png)|*.jpg;*.jpeg;.*.gif;*.png";
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
            // Tùy position chọn INSERT vào Login hay HR
            if (rbt_Role.Checked)
                command = new SqlCommand(
                    "INSERT INTO HR (MSGV,Fname,Lname,Position,Username,Pass,Email,Pic,Valid) " +
                    "VALUES (@id,@fn,@ln,@pos,@user,@pass,@email,@pic,@val)", my_db.getConnection);
            else
                command = new SqlCommand(
                    "INSERT INTO Login (ID,Fname,Lname,Position,Username,Pass,Email,Pic,Valid) " +
                    "VALUES (@id,@fn,@ln,@pos,@user,@pass,@email,@pic,@val)", my_db.getConnection);

            command.Parameters.Add("@id", SqlDbType.Int).Value = int.Parse(txb_ID.Text);
            command.Parameters.Add("@fn", SqlDbType.NVarChar).Value = txb_Fname.Text;
            command.Parameters.Add("@ln", SqlDbType.NVarChar).Value = txb_Lname.Text;
            command.Parameters.Add("@pos", SqlDbType.Int).Value = position;
            command.Parameters.Add("@user", SqlDbType.VarChar).Value = txb_User.Text;
            command.Parameters.Add("@pass", SqlDbType.VarChar).Value = txb_Pass.Text;
            command.Parameters.Add("@email", SqlDbType.VarChar).Value = txb_Email.Text;
            command.Parameters.Add("@pic", SqlDbType.Image).Value = picture.ToArray();
            command.Parameters.Add("@val", SqlDbType.Int).Value = 0;  // Chờ duyệt

            my_db.openConnection();
            bool result = command.ExecuteNonQuery() == 1;
            my_db.closeConnection();
            return result;
        }
        
        bool existUser()
        {
            My_DB my_db = new My_DB();
            SqlCommand cmd = new SqlCommand(
                "SELECT Count(*) FROM Login,HR WHERE Login.Username LIKE @user OR HR.Username LIKE @user",
                my_db.getConnection);
            cmd.Parameters.Add("@user", SqlDbType.NVarChar).Value = txb_User.Text.Trim();
            my_db.openConnection();
            bool exists = (int)cmd.ExecuteScalar() > 0;
            my_db.closeConnection();
            return !exists;   // true = chưa tồn tại (được dùng)
        }

        // Tự viết thêm hàm kiểm tra Email
        bool existEmail()
        {
            My_DB my_db = new My_DB();
            SqlCommand cmd = new SqlCommand(
                "SELECT Count(*) FROM Login,HR WHERE Login.Email LIKE @email OR HR.Email LIKE @email",
                my_db.getConnection);
            cmd.Parameters.Add("@email", SqlDbType.NVarChar).Value = txb_Email.Text.Trim();
            my_db.openConnection();
            bool exists = (int)cmd.ExecuteScalar() > 0;
            my_db.closeConnection();
            return !exists;
        }

        // Tự viết thêm hàm kiểm tra nhập liệu
        bool verif()
        {
            if (txb_ID.Text.Trim() == "" || txb_Fname.Text.Trim() == "" ||
                txb_Lname.Text.Trim() == "" || txb_User.Text.Trim() == "" ||
                txb_Pass.Text.Trim() == "" || txb_Email.Text.Trim() == "" ||
                ptb_Picture.Image == null)
            {
                return false;
            }
            return true;
        }
        string sendOTP(string targetEmail)
        {
            Random rand = new Random();
            string otpCode = rand.Next(100000, 999999).ToString();

            try
            {
                var fromAddress = new MailAddress("duongduyvinh206@gmail.com", "Hệ Thống Quản Lý");
                var toAddress = new MailAddress(targetEmail);
                string fromPassword = "bsxxalktelewxuwo"; // Mật khẩu ứng dụng bạn vừa lấy

                string subject = "Mã xác thực đăng ký";
                string body = $"Mã OTP của bạn là: {otpCode}";

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
                MessageBox.Show("Lỗi gửi Email: " + ex.Message);
                return null;
            }
        }

        private void f_Register_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn thoát không?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.No)
            {
                e.Cancel = true; // Hủy bỏ lệnh đóng, Form sẽ giữ nguyên
            }
            else
            {
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
    }
}
