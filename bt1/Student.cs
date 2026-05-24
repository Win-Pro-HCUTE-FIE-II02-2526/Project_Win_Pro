using bt1;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class Student
{
    My_DB db = new My_DB();

    public int MSSV { get; set; }
    public string Fname { get; set; }
    public string Lname { get; set; }
    public DateTime Dob { get; set; }
    public string Gender { get; set; }
    public string Phone { get; set; }
    public string Address { get; set; }
    public string Hometown { get; set; }
    public string Email { get; set; }
    public byte[] Picture { get; set; }

    // Constructor
    public Student(int mssv, string fname, string lname, DateTime dob,
        string gender, string phone, string address, string hometown,
        string email, byte[] picture)
    {
        MSSV = mssv; Fname = fname; Lname = lname; Dob = dob;
        Gender = gender; Phone = phone; Address = address;
        Hometown = hometown; Email = email; Picture = picture;
    }

    public bool AddStudent()
    {
        try
        {
            db.openConnection();
            string query = "INSERT INTO Student VALUES " +
                "(@mssv, @fname, @lname, @dob, @gder, @phone, @addr, @htown, @email, @pic)";
            SqlCommand cmd = new SqlCommand(query, db.conn);
            cmd.Parameters.AddWithValue("@mssv", MSSV);
            cmd.Parameters.AddWithValue("@fname", Fname);
            cmd.Parameters.AddWithValue("@lname", Lname);
            cmd.Parameters.AddWithValue("@dob", Dob);
            cmd.Parameters.AddWithValue("@gder", Gender);
            cmd.Parameters.AddWithValue("@phone", Phone);
            cmd.Parameters.AddWithValue("@addr", Address);
            cmd.Parameters.AddWithValue("@htown", Hometown);
            cmd.Parameters.AddWithValue("@email", Email);
            cmd.Parameters.AddWithValue("@pic", (object)Picture ?? DBNull.Value);

            int rows = cmd.ExecuteNonQuery();
            return rows > 0;
        }
        catch { return false; }
        finally { db.closeConnection(); }
    }
    public bool DeleteStudent()
    {
        try
        {
            // 1. Xóa điểm liên quan trước để tránh lỗi khóa ngoại (Foreign Key)
            SqlCommand cmd2 = new SqlCommand("DELETE FROM Score WHERE MSSV=@id", db.getConnection);
            cmd2.Parameters.Add("@id", SqlDbType.Int).Value = MSSV;
            db.openConnection(); cmd2.ExecuteNonQuery();

            // 2. Xóa đăng ký môn học liên quan
            SqlCommand cmd1 = new SqlCommand("DELETE FROM DKMH WHERE MSSV=@id", db.getConnection);
            cmd1.Parameters.Add("@id", SqlDbType.Int).Value = MSSV;
            cmd1.ExecuteNonQuery();

            // 3. Xóa thông tin sinh viên chính thức
            SqlCommand cmd = new SqlCommand("DELETE FROM Student WHERE MSSV=@id", db.getConnection);
            cmd.Parameters.Add("@id", SqlDbType.Int).Value = MSSV;
            bool ok = cmd.ExecuteNonQuery() == 1;
            return ok;
        }
        catch { return false; }
        finally { db.closeConnection(); }
    }
    public static DataTable GetStudents()
    {
        My_DB db = new My_DB();
        DataTable dt = new DataTable();
        try
        {
            db.openConnection();
            string query = "SELECT MSSV, Fname, Lname, Dob, Gder, Phone, Address, Htown, Email FROM Student";
            SqlDataAdapter adapter = new SqlDataAdapter(query, db.conn);
            adapter.Fill(dt);
        }
        finally { db.closeConnection(); }
        return dt;
    }
}