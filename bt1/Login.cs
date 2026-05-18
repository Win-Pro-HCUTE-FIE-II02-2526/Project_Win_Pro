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
            My_DB db = new My_DB();
            SqlDataAdapter adapter = new SqlDataAdapter();
            DataTable table = new DataTable();
            SqlCommand command = new SqlCommand("SELECT * FROM Login WHERE UserName = @User AND Password = @Password", db.getConnection);
            command.Parameters.Add("@User", SqlDbType.NVarChar).Value = txt_UserName.Text;
            command.Parameters.Add("@Password", SqlDbType.NVarChar).Value = txt_Password.Text;
            adapter.SelectCommand = command;
            adapter.Fill(table);
            if (table.Rows.Count > 0)
            {
                DataRow row = table.Rows[0];
                Globals.SetGlobalUserInfo(
                    Convert.ToInt32(row["Id"]),
                    row["UserName"].ToString(),
                    row["Role"].ToString(),
                    row["Email"].ToString()
                    );
                MessageBox.Show($"Chào mừng {Globals.GlobalUserName}! Đăng nhập thành công.", "Thông báo");
                f_AddStudent add = new f_AddStudent();
                add.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("UserName/Password không đúng vui lòng thử lại!", "Login Error", MessageBoxButtons.OK, MessageBoxIcon.Stop);
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
            f_Register fRegister = new f_Register();
            fRegister.Show();
            this.Hide();
        }
    }
      
}
