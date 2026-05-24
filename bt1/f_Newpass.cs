using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.RegularExpressions;

namespace bt1
{
    public partial class f_Newpass : Form
    {
        // Biến public để nhận giá trị địa chỉ Email từ form f_Forget truyền sang
        public string email { get; set; }

        public f_Newpass()
        {
            InitializeComponent();

            // Ép cấu hình xóa bỏ text mặc định cứng và đẩy vào Placeholder để tăng trải nghiệm UI/UX
            txb_Newpass.DefaultText = "";
            txb_Newpass.PlaceholderText = "Nhập mật khẩu mới";
            txb_ConfirmNewpass.DefaultText = "";
            txb_ConfirmNewpass.PlaceholderText = "Xác nhận mật khẩu mới";

            // Liên kết sự kiện click và sự kiện đổi chữ trực tiếp bằng mã lệnh
            this.btn_Changepass.Click += new System.EventHandler(this.btn_Changepass_Click);
            this.txb_ConfirmNewpass.TextChanged += new System.EventHandler(this.txb_ConfirmNewpass_TextChanged);
        }

        private void btn_Changepass_Click(object sender, EventArgs e)
        {
            string newPassword = txb_Newpass.Text.Trim();
            string confirmPassword = txb_ConfirmNewpass.Text.Trim();

            // 1. Kiểm tra dữ liệu rỗng đầu vào
            if (string.IsNullOrEmpty(newPassword) || string.IsNullOrEmpty(confirmPassword))
            {
                MessageBox.Show("Vui lòng điền đầy đủ thông tin vào các ô mật khẩu!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // 2. Kiểm tra tính trùng khớp của hai ô mật khẩu
            if (newPassword != confirmPassword)
            {
                MessageBox.Show("Mật khẩu xác nhận không trùng khớp! Vui lòng kiểm tra lại.", "Lỗi nhập liệu", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txb_ConfirmNewpass.Focus();
                return;
            }

            // 3. Kiểm tra độ mạnh mật khẩu bằng Regex [Tính năng Nâng cao nâng điểm đồ án]
            if (!IsStrongPassword(newPassword))
            {
                MessageBox.Show("Mật khẩu phải chứa tối thiểu 8 ký tự, bao gồm ít nhất 1 chữ in hoa, 1 chữ số và 1 ký tự đặc biệt!", "Mật khẩu quá yếu", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txb_Newpass.Focus();
                return;
            }

            // 4. Thực hiện cập nhật mật khẩu mới vào cơ sở dữ liệu
            if (UpdatePasswordToDatabase(newPassword))
            {
                // Trả về kết quả OK để báo hiệu cho form f_Forget hiển thị thông báo thành công
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            else
            {
                MessageBox.Show("Quá trình cập nhật mật khẩu mới thất bại! Vui lòng thử lại.", "Lỗi hệ thống", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Hàm kiểm tra so sánh Realtime (thời gian thực) đổi màu viền ô nhập khi người dùng gõ phím
        private void txb_ConfirmNewpass_TextChanged(object sender, EventArgs e)
        {
            if (txb_Newpass.Text == txb_ConfirmNewpass.Text && !string.IsNullOrEmpty(txb_ConfirmNewpass.Text))
            {
                txb_ConfirmNewpass.BorderColor = Color.Green; // Viền xanh lá biểu thị khớp hoàn toàn
                txb_ConfirmNewpass.FocusedState.BorderColor = Color.Green;
            }
            else
            {
                txb_ConfirmNewpass.BorderColor = Color.Red;   // Viền đỏ biểu thị không khớp hoặc trống
                txb_ConfirmNewpass.FocusedState.BorderColor = Color.Red;
            }
        }

        // Thuật toán kiểm tra độ mạnh mật khẩu bằng Regular Expression
        private bool IsStrongPassword(string password)
        {
            // Tối thiểu 8 ký tự, 1 hoa, 1 số, 1 ký tự đặc biệt
            string pattern = @"^(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$";
            return Regex.IsMatch(password, pattern);
        }

        // Hàm thực thi cập nhật đồng bộ mật khẩu mới cho cả bảng Login và bảng HR dựa vào Email liên kết
        private bool UpdatePasswordToDatabase(string newPass)
        {
            My_DB my_db = new My_DB();
            bool updateLoginSuccess = false;
            bool updateHRSuccess = false;

            // Câu lệnh UPDATE mật khẩu cho bảng Login (Sử dụng cột Password chuẩn của bạn)
            string queryLogin = "UPDATE Login SET Password = @pass WHERE Email = @email";
            SqlCommand cmdLogin = new SqlCommand(queryLogin, my_db.getConnection);
            cmdLogin.Parameters.Add("@pass", SqlDbType.VarChar).Value = newPass;
            cmdLogin.Parameters.Add("@email", SqlDbType.NVarChar).Value = this.email;

            // Câu lệnh UPDATE mật khẩu cho bảng HR (Sử dụng cột Password sau khi đã sp_rename đồng bộ)
            string queryHR = "UPDATE HR SET Password = @pass WHERE Email = @email";
            SqlCommand cmdHR = new SqlCommand(queryHR, my_db.getConnection);
            cmdHR.Parameters.Add("@pass", SqlDbType.VarChar).Value = newPass;
            cmdHR.Parameters.Add("@email", SqlDbType.NVarChar).Value = this.email;

            try
            {
                my_db.openConnection();

                // Thực thi cập nhật cho cả hai bảng một cách độc lập
                int rowsLogin = cmdLogin.ExecuteNonQuery();
                if (rowsLogin > 0) updateLoginSuccess = true;

                int rowsHR = cmdHR.ExecuteNonQuery();
                if (rowsHR > 0) updateHRSuccess = true;

                // Trả về thành công nếu có ít nhất một tài khoản ở một trong hai vai trò được cập nhật mật khẩu mới
                return (updateLoginSuccess || updateHRSuccess);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi thực thi dữ liệu cập nhật: " + ex.Message, "Lỗi Database");
                return false;
            }
            finally
            {
                my_db.closeConnection();
            }
        }
    }
}