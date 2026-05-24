using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace bt1
{
    public partial class f_OTP : Form
    {
        public string receivedOTP;     // Biến lưu mã OTP đang hiệu lực
        public string destinationEmail; // Lưu Email nhận để phục vụ chức năng gửi lại mã
        private DateTime timeCreated;   // Mốc thời gian sinh mã để kiểm tra thời hạn 5 phút [cite: 131]

        // Cập nhật lại Constructor nhận 2 tham số đầu vào
        public f_OTP(string otp, string email)
        {
            InitializeComponent();
            this.receivedOTP = otp;
            this.destinationEmail = email;
            this.timeCreated = DateTime.Now; // Ghi nhận thời điểm tạo mã lần đầu
        }

        private void btn_Verify_Click(object sender, EventArgs e)
        {
            // 1. Kiểm tra thời hạn hiệu lực của OTP (Quá 5 phút sẽ từ chối) [cite: 131]
            if ((DateTime.Now - timeCreated).TotalMinutes > 5)
            {
                MessageBox.Show("Mã OTP này đã hết hạn (quá 5 phút)! Vui lòng nhấn nút 'Gửi lại' để lấy mã mới.", "Mã hết hạn", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // 2. Xác thực mã người dùng nhập vào
            if (txt_Code.Text.Trim() == receivedOTP)
            {
                MessageBox.Show("Xác thực mã OTP thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information); 
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            else
            {
                MessageBox.Show("Mã OTP không chính xác. Vui lòng kiểm tra kỹ lại!", "Lỗi xác thực", MessageBoxButtons.OK, MessageBoxIcon.Error); 
                txt_Code.Clear();
                txt_Code.Focus();
            }
        }

        private void btn_Resend_Click(object sender, EventArgs e)
        {
            // Tìm và tham chiếu lại Form Register đang ẩn để dùng hàm gửi mail có sẵn
            f_Register registerForm = (f_Register)Application.OpenForms["f_Register"];

            if (registerForm != null)
            {
                this.Cursor = Cursors.WaitCursor; // Đổi con trỏ chuột biểu thị đang tải dữ liệu ròng
                string newOTP = registerForm.sendOTP(destinationEmail);
                this.Cursor = Cursors.Default;

                if (newOTP != null)
                {
                    this.receivedOTP = newOTP;     // Cập nhật mã OTP mới vào hệ thống
                    this.timeCreated = DateTime.Now; // Đặt lại mốc đếm thời gian 5 phút mới [cite: 131]
                    txt_Code.Clear();
                    MessageBox.Show("Mã OTP mới đã được gửi thành công tới Email của bạn!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
            {
                MessageBox.Show("Hệ thống mất liên kết, vui lòng thực hiện lại từ bước đăng ký!", "Lỗi hệ thống");
            }
        }
    }
}