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

namespace bt1
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }

        private void bt_Cancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void bt_Login_Click(object sender, EventArgs e)
        {
            // 1. Kiểm tra người dùng đã nhập liệu chưa
            if (string.IsNullOrWhiteSpace(txt_UserName.Text) || string.IsNullOrWhiteSpace(txt_Password.Text))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ Tài khoản và Mật khẩu!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            My_DB db = new My_DB();
            SqlDataAdapter adapter = new SqlDataAdapter();
            DataTable table = new DataTable();

            // 2. Tự động chuyển đổi bảng quét SQL dựa theo việc tích chọn rbt_Role
            string query = "";
            if (rbt_Role.Checked)
            {
                // Nếu chọn rbt_Role (Tài khoản Nhân sự HR) -> Quét bảng HR
                // Kiểm tra thêm điều kiện Valid = 1 để chắc chắn tài khoản đã được duyệt mới cho vào [cite: 6]
                query = "SELECT * FROM HR WHERE Username = @User AND Pass = @Password AND Valid = 1";
            }
            else
            {
                // Ngược lại nếu không chọn -> Quét bảng Login (Tài khoản Student hoặc Admin)
                query = "SELECT * FROM Login WHERE UserName = @User AND Password = @Password";
            }

            SqlCommand command = new SqlCommand(query, db.getConnection);
            command.Parameters.Add("@User", SqlDbType.NVarChar).Value = txt_UserName.Text.Trim();
            command.Parameters.Add("@Password", SqlDbType.NVarChar).Value = txt_Password.Text.Trim();

            try
            {
                adapter.SelectCommand = command;
                adapter.Fill(table);

                if (table.Rows.Count > 0)
                {
                    DataRow row = table.Rows[0];

                    // 3. Đọc dữ liệu ID thông minh (bảng HR dùng cột MSGV, bảng Login dùng cột Id)
                    int userId = rbt_Role.Checked ? Convert.ToInt32(row["MSGV"]) : Convert.ToInt32(row["Id"]);
                    string role = row["Role"] != DBNull.Value ? row["Role"].ToString() : (rbt_Role.Checked ? "HR" : "Student");

                    // 4. Lưu thông tin phiên đăng nhập vào hệ thống lớp dùng chung Globals
                    Globals.SetGlobalUserInfo(
                        userId,
                        row["UserName"].ToString(),
                        role,
                        row["Email"].ToString()
                    );

                    MessageBox.Show($"Chào mừng {Globals.GlobalUserName}! Đăng nhập thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // 5. Mở thẳng form f_AddStudent theo đúng cấu trúc dự án hiện tại của bạn
                    f_AddStudent add = new f_AddStudent();
                    add.Show();
                    this.Hide();
                }
                else
                {
                    // Đưa ra cảnh báo chi tiết để sinh viên dễ nhận biết lý do đăng nhập thất bại
                    string errorMsg = rbt_Role.Checked
                        ? "Tài khoản/Mật khẩu HR chưa chính xác hoặc đang chờ kiểm duyệt!"
                        : "Tài khoản hoặc Mật khẩu không đúng, vui lòng thử lại!";
                    MessageBox.Show(errorMsg, "Lỗi đăng nhập", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi hệ thống khi đăng nhập: " + ex.Message, "Lỗi kết nối");
            }
        }

        private void Login_Load(object sender, EventArgs e)
        {
            try
            {
                picturebox1.Image = Image.FromFile(@"D:\Window_Progamming\Phan-Mem-Quan-Ly-Sinh-Vien_2\bt1\Images\LOGO_HCMUTE.png");
                picturebox1.SizeMode = PictureBoxSizeMode.Zoom;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi load ảnh: " + ex.Message);
            }
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            // Truyền trạng thái sang Form Register: Nếu chọn rbt_Role thì truyền quyền HR (2), ngược lại truyền Student (1) [cite: 3]
            int registerPosition = rbt_Role.Checked ? 2 : 1;

            f_Register fRegister = new f_Register(registerPosition);
            fRegister.Show();
            this.Hide();
        }

        private void rbt_Role_CheckedChanged(object sender, EventArgs e)
        {
            // Để trống
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            f_Forget fForget = new f_Forget();
            fForget.Show();
            this.Hide();
        }
    }
}