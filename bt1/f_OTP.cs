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
        public string receivedOTP; // Biến lưu mã OTP hệ thống gửi đi

        // Sửa Constructor để nhận mã
        public f_OTP(string otp)
        {
            InitializeComponent();
            this.receivedOTP = otp;
        }

        private void btn_Verify_Click(object sender, EventArgs e)
        {
            // 1. Kiểm tra người dùng nhập đúng hay sai
            if (txt_Code.Text.Trim() == receivedOTP)
            {
                MessageBox.Show("Xác thực thành công!", "Thông báo");
                this.DialogResult = DialogResult.OK; // Trả về OK để Form Register thực hiện hàm Register()
                this.Close();
            }
            else
            {
                MessageBox.Show("Mã OTP không chính xác. Vui lòng thử lại!", "Lỗi");
                txt_Code.Clear();
                txt_Code.Focus();
            }
        }
    }
}
