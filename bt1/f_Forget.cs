using System;
using System.Data;
using System.Data.SqlClient;
using System.Net;
using System.Net.Mail;
using System.Windows.Forms;

namespace bt1
{
    public partial class f_Forget : Form
    {
        public f_Forget()
        {
            InitializeComponent();
            // Gắn sự kiện click cho nút Tiếp tục bằng code để đảm bảo liên kết hoạt động
            this.btn_Accept.Click += new System.EventHandler(this.btn_Accept_Click);
        }

        private void btn_Accept_Click(object sender, EventArgs e)
        {
            string emailInput = txb_Email.Text.Trim();

            // 1. Kiểm tra người dùng đã nhập Email chưa
            if (string.IsNullOrEmpty(emailInput))
            {
                MessageBox.Show("Vui lòng nhập địa chỉ Email khôi phục!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txb_Email.Focus();
                return;
            }

            // 2. Kiểm tra sự tồn tại của Email trong hệ thống Database
            if (CheckEmail(emailInput))
            {
                this.Cursor = Cursors.WaitCursor; // Đổi con trỏ chuột sang trạng thái chờ
                string generatedOTP = SendOTPEmail(emailInput);
                this.Cursor = Cursors.Default;

                // Kiểm tra xem hệ thống SMTP có gửi được thư đi thành công hay không
                if (generatedOTP != null)
                {
                    // Khởi tạo và nạp tham số sang Form OTP
                    f_OTP otpForm = new f_OTP(generatedOTP, emailInput);
                    this.Hide(); // Ẩn form nhập email khôi phục

                    // Hiển thị Form OTP dưới dạng hộp thoại bắt buộc (Modal Dialog)
                    if (otpForm.ShowDialog() == DialogResult.OK)
                    {
                        // Chuyển tiếp sang Form thiết lập mật khẩu mới khi OTP hợp lệ
                        f_Newpass newpassForm = new f_Newpass();
                        newpassForm.email = emailInput;

                        if (newpassForm.ShowDialog() == DialogResult.OK)
                        {
                            MessageBox.Show("Đổi mật khẩu thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            this.Close(); // Đóng hẳn chuỗi khôi phục
                            return;
                        }
                    }

                    // Nếu người dùng đóng hoặc hủy giữa chừng, hiện lại form hiện tại
                    this.Show();
                }
                else
                {
                    // Lỗi xảy ra khi không kết nối được mạng hoặc cấu hình SMTP Gmail sai mật khẩu ứng dụng
                    MessageBox.Show("Hệ thống không thể gửi mã OTP. Vui lòng kiểm tra lại kết nối mạng!", "Lỗi hệ thống", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                // Nhánh else chuẩn của CheckEmail: Email hoàn toàn không tồn tại trong DB
                MessageBox.Show("Email không tồn tại trong hệ thống!", "Lỗi xác thực", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txb_Email.Focus();
            }
        }
        // Hàm kiểm tra Email tồn tại ở cả bảng Login VÀ bảng HR [cite: 137, 166]
        private bool CheckEmail(string email)
        {
            My_DB my_db = new My_DB();

            // Sử dụng truy vấn Subquery độc lập để loại bỏ Cross Join giúp tăng tốc độ truy vấn
            string query = "SELECT (SELECT Count(*) FROM Login WHERE Email = @email) + (SELECT Count(*) FROM HR WHERE Email = @email)";

            SqlCommand cmd = new SqlCommand(query, my_db.getConnection); // Sử dụng đồng bộ thuộc tính getConnection [cite: 169]
            cmd.Parameters.Add("@email", SqlDbType.NVarChar).Value = email;

            try
            {
                my_db.openConnection();
                int totalCount = (int)cmd.ExecuteScalar();
                return totalCount > 0; // Trả về true nếu tìm thấy email trong hệ thống [cite: 172, 174]
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi kiểm tra dữ liệu kết nối: " + ex.Message, "Lỗi SQL");
                return false;
            }
            finally
            {
                my_db.closeConnection();
            }
        }

        // Hàm sinh mã ngẫu nhiên và gửi mã OTP khôi phục qua Gmail SMTP [cite: 84]
        private string SendOTPEmail(string targetEmail)
        {
            Random rand = new Random();
            string otpCode = rand.Next(100000, 999999).ToString(); // Tạo mã OTP 6 chữ số ngẫu nhiên [cite: 84, 97]

            try
            {
                // Cấu hình tài khoản gửi thư hệ thống của bạn [cite: 90, 91]
                var fromAddress = new MailAddress("duongduyvinh206@gmail.com", "Hệ Thống Quản Lý");
                var toAddress = new MailAddress(targetEmail);
                string fromPassword = "bsxxalktelewxuwo"; // Mật khẩu ứng dụng của bạn

                string subject = "Mã OTP khôi phục mật khẩu tài khoản";
                string body = $"Hệ thống nhận được yêu cầu lấy lại mật khẩu của bạn.\n\nMã OTP xác thực là: {otpCode}\nLưu ý: Mã này chỉ có hiệu lực trong vòng 5 phút.";

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
                MessageBox.Show("Lỗi gửi mail hệ thống: " + ex.Message, "Lỗi gửi OTP");
                return null;
            }
        }
    }
}