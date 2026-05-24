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
    public partial class f_ListStudent : Form
    {
        public f_ListStudent()
        {
            InitializeComponent();
        }
        

        

        private void LoadStudentList()
        {
            dgvStudents.DataSource = Student.GetStudents();
            dgvStudents.Columns["MSSV"].HeaderText = "Mã SV";
            dgvStudents.Columns["Fname"].HeaderText = "Họ";
            dgvStudents.Columns["Lname"].HeaderText = "Tên";
            dgvStudents.Columns["Dob"].HeaderText = "Ngày sinh";
            dgvStudents.Columns["Gder"].HeaderText = "Giới tính";
        }



        private void btnAdd_Click(object sender, EventArgs e)
        {
            f_AddStudent f_Add = new f_AddStudent();
            f_Add.Show();
            this.Hide();
        }

        private void f_ListStudent_Load(object sender, EventArgs e)
        {
            LoadStudentList();
        }

        private void btnReload_Click(object sender, EventArgs e)
        {
            txtSearch.Clear();
            LoadStudentList();
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        { 
            My_DB db = new My_DB();
            DataTable dt = new DataTable();
            try
            {
                db.openConnection();
                string query = "SELECT * FROM Student WHERE " +
                    "CAST(MSSV AS NVARCHAR) LIKE @search OR " +
                    "Fname LIKE @search OR Lname LIKE @search";
                SqlCommand cmd = new SqlCommand(query, db.conn);
                cmd.Parameters.AddWithValue("@search", "%" + txtSearch.Text + "%");
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                adapter.Fill(dt);
                dgvStudents.DataSource = dt;
            }
            finally { db.closeConnection(); }
        }
    }
}
