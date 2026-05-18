using System.Windows.Forms;
namespace bt1
{
    partial class f_AddStudent
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.txtMSSV = new Guna.UI2.WinForms.Guna2TextBox();
            this.txtFname = new Guna.UI2.WinForms.Guna2TextBox();
            this.txtLname = new Guna.UI2.WinForms.Guna2TextBox();
            this.dtpDob = new Guna.UI2.WinForms.Guna2DateTimePicker();
            this.cboGender = new Guna.UI2.WinForms.Guna2ComboBox();
            this.txtPhone = new Guna.UI2.WinForms.Guna2TextBox();
            this.txtAddress = new Guna.UI2.WinForms.Guna2TextBox();
            this.txtHometown = new Guna.UI2.WinForms.Guna2TextBox();
            this.txtEmail = new Guna.UI2.WinForms.Guna2TextBox();
            this.btnChooseImage = new Guna.UI2.WinForms.Guna2Button();
            this.btnAdd = new Guna.UI2.WinForms.Guna2Button();
            this.btnClear = new Guna.UI2.WinForms.Guna2Button();
            this.picStudent = new Guna.UI2.WinForms.Guna2PictureBox();
            this.btnLogout = new Guna.UI2.WinForms.Guna2Button();
            ((System.ComponentModel.ISupportInitialize)(this.picStudent)).BeginInit();
            this.SuspendLayout();
            // 
            // txtMSSV
            // 
            this.txtMSSV.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtMSSV.DefaultText = "";
            this.txtMSSV.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtMSSV.Location = new System.Drawing.Point(240, 30);
            this.txtMSSV.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtMSSV.Name = "txtMSSV";
            this.txtMSSV.PlaceholderText = "Mã số sinh viên";
            this.txtMSSV.SelectedText = "";
            this.txtMSSV.Size = new System.Drawing.Size(260, 40);
            this.txtMSSV.TabIndex = 13;
            // 
            // txtFname
            // 
            this.txtFname.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtFname.DefaultText = "";
            this.txtFname.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtFname.Location = new System.Drawing.Point(240, 85);
            this.txtFname.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtFname.Name = "txtFname";
            this.txtFname.PlaceholderText = "Họ và chữ lót";
            this.txtFname.SelectedText = "";
            this.txtFname.Size = new System.Drawing.Size(260, 40);
            this.txtFname.TabIndex = 12;
            // 
            // txtLname
            // 
            this.txtLname.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtLname.DefaultText = "";
            this.txtLname.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtLname.Location = new System.Drawing.Point(240, 140);
            this.txtLname.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtLname.Name = "txtLname";
            this.txtLname.PlaceholderText = "Tên sinh viên";
            this.txtLname.SelectedText = "";
            this.txtLname.Size = new System.Drawing.Size(260, 40);
            this.txtLname.TabIndex = 11;
            // 
            // dtpDob
            // 
            this.dtpDob.BorderRadius = 8;
            this.dtpDob.Checked = true;
            this.dtpDob.FillColor = System.Drawing.Color.White;
            this.dtpDob.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.dtpDob.Format = System.Windows.Forms.DateTimePickerFormat.Long;
            this.dtpDob.Location = new System.Drawing.Point(240, 195);
            this.dtpDob.MaxDate = new System.DateTime(9998, 12, 31, 0, 0, 0, 0);
            this.dtpDob.MinDate = new System.DateTime(1753, 1, 1, 0, 0, 0, 0);
            this.dtpDob.Name = "dtpDob";
            this.dtpDob.Size = new System.Drawing.Size(260, 40);
            this.dtpDob.TabIndex = 10;
            this.dtpDob.Value = new System.DateTime(2026, 5, 13, 2, 5, 3, 413);
            // 
            // cboGender
            // 
            this.cboGender.BackColor = System.Drawing.Color.Transparent;
            this.cboGender.BorderRadius = 8;
            this.cboGender.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cboGender.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboGender.FocusedColor = System.Drawing.Color.Empty;
            this.cboGender.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.cboGender.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(88)))), ((int)(((byte)(112)))));
            this.cboGender.ItemHeight = 30;
            this.cboGender.Items.AddRange(new object[] {
            "Nam",
            "Nữ"});
            this.cboGender.Location = new System.Drawing.Point(520, 195);
            this.cboGender.Name = "cboGender";
            this.cboGender.Size = new System.Drawing.Size(260, 36);
            this.cboGender.TabIndex = 9;
            // 
            // txtPhone
            // 
            this.txtPhone.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtPhone.DefaultText = "";
            this.txtPhone.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtPhone.Location = new System.Drawing.Point(520, 30);
            this.txtPhone.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtPhone.Name = "txtPhone";
            this.txtPhone.PlaceholderText = "Số điện thoại";
            this.txtPhone.SelectedText = "";
            this.txtPhone.Size = new System.Drawing.Size(260, 40);
            this.txtPhone.TabIndex = 8;
            // 
            // txtAddress
            // 
            this.txtAddress.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtAddress.DefaultText = "";
            this.txtAddress.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtAddress.Location = new System.Drawing.Point(240, 250);
            this.txtAddress.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtAddress.Multiline = true;
            this.txtAddress.Name = "txtAddress";
            this.txtAddress.PlaceholderText = "Địa chỉ thường trú chi tiết...";
            this.txtAddress.SelectedText = "";
            this.txtAddress.Size = new System.Drawing.Size(540, 80);
            this.txtAddress.TabIndex = 7;
            // 
            // txtHometown
            // 
            this.txtHometown.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtHometown.DefaultText = "";
            this.txtHometown.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtHometown.Location = new System.Drawing.Point(520, 140);
            this.txtHometown.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtHometown.Name = "txtHometown";
            this.txtHometown.PlaceholderText = "Quê quán";
            this.txtHometown.SelectedText = "";
            this.txtHometown.Size = new System.Drawing.Size(260, 40);
            this.txtHometown.TabIndex = 6;
            // 
            // txtEmail
            // 
            this.txtEmail.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtEmail.DefaultText = "";
            this.txtEmail.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtEmail.Location = new System.Drawing.Point(520, 85);
            this.txtEmail.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtEmail.Name = "txtEmail";
            this.txtEmail.PlaceholderText = "Địa chỉ Email";
            this.txtEmail.SelectedText = "";
            this.txtEmail.Size = new System.Drawing.Size(260, 40);
            this.txtEmail.TabIndex = 5;
            // 
            // btnChooseImage
            // 
            this.btnChooseImage.BorderRadius = 8;
            this.btnChooseImage.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnChooseImage.ForeColor = System.Drawing.Color.White;
            this.btnChooseImage.Location = new System.Drawing.Point(30, 260);
            this.btnChooseImage.Name = "btnChooseImage";
            this.btnChooseImage.Size = new System.Drawing.Size(180, 40);
            this.btnChooseImage.TabIndex = 4;
            this.btnChooseImage.Text = "Chọn hình ảnh";
            this.btnChooseImage.Click += new System.EventHandler(this.btnChooseImage_Click);
            // 
            // btnAdd
            // 
            this.btnAdd.BorderRadius = 10;
            this.btnAdd.FillColor = System.Drawing.Color.LimeGreen;
            this.btnAdd.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.btnAdd.ForeColor = System.Drawing.Color.White;
            this.btnAdd.Location = new System.Drawing.Point(580, 415);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(200, 50);
            this.btnAdd.TabIndex = 3;
            this.btnAdd.Text = "THÊM SINH VIÊN";
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // btnClear
            // 
            this.btnClear.BorderRadius = 10;
            this.btnClear.FillColor = System.Drawing.Color.Silver;
            this.btnClear.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnClear.ForeColor = System.Drawing.Color.White;
            this.btnClear.Location = new System.Drawing.Point(440, 415);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(130, 50);
            this.btnClear.TabIndex = 2;
            this.btnClear.Text = "Làm mới";
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // picStudent
            // 
            this.picStudent.BorderRadius = 12;
            this.picStudent.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(245)))), ((int)(((byte)(250)))));
            this.picStudent.ImageRotate = 0F;
            this.picStudent.Location = new System.Drawing.Point(30, 30);
            this.picStudent.Name = "picStudent";
            this.picStudent.Size = new System.Drawing.Size(180, 220);
            this.picStudent.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picStudent.TabIndex = 1;
            this.picStudent.TabStop = false;
            // 
            // btnLogout
            // 
            this.btnLogout.BackColor = System.Drawing.Color.RosyBrown;
            this.btnLogout.BorderRadius = 8;
            this.btnLogout.FillColor = System.Drawing.Color.LightCoral;
            this.btnLogout.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnLogout.ForeColor = System.Drawing.Color.White;
            this.btnLogout.Location = new System.Drawing.Point(30, 430);
            this.btnLogout.Name = "btnLogout";
            this.btnLogout.Size = new System.Drawing.Size(100, 35);
            this.btnLogout.TabIndex = 0;
            this.btnLogout.Text = "Đăng xuất";
            this.btnLogout.Click += new System.EventHandler(this.btnLogout_Click);
            // 
            // f_AddStudent
            // 
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(810, 490);
            this.Controls.Add(this.btnLogout);
            this.Controls.Add(this.picStudent);
            this.Controls.Add(this.btnClear);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.btnChooseImage);
            this.Controls.Add(this.txtEmail);
            this.Controls.Add(this.txtHometown);
            this.Controls.Add(this.txtAddress);
            this.Controls.Add(this.txtPhone);
            this.Controls.Add(this.cboGender);
            this.Controls.Add(this.dtpDob);
            this.Controls.Add(this.txtLname);
            this.Controls.Add(this.txtFname);
            this.Controls.Add(this.txtMSSV);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "f_AddStudent";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Hệ thống quản lý: Thêm sinh viên mới";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.f_AddStudent_FormClosed);
            ((System.ComponentModel.ISupportInitialize)(this.picStudent)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Guna.UI2.WinForms.Guna2TextBox txtMSSV;
        private Guna.UI2.WinForms.Guna2TextBox txtFname;
        private Guna.UI2.WinForms.Guna2TextBox txtLname;
        private Guna.UI2.WinForms.Guna2DateTimePicker dtpDob;
        private Guna.UI2.WinForms.Guna2ComboBox cboGender;
        private Guna.UI2.WinForms.Guna2TextBox txtPhone;
        private Guna.UI2.WinForms.Guna2TextBox txtAddress;
        private Guna.UI2.WinForms.Guna2TextBox txtHometown;
        private Guna.UI2.WinForms.Guna2TextBox txtEmail;
        private Guna.UI2.WinForms.Guna2Button btnChooseImage;
        private Guna.UI2.WinForms.Guna2Button btnAdd;
        private Guna.UI2.WinForms.Guna2Button btnClear;
        private Guna.UI2.WinForms.Guna2PictureBox picStudent;
        private Guna.UI2.WinForms.Guna2Button btnLogout;
        private Guna.UI2.WinForms.Guna2ShadowForm guna2ShadowForm1;
    }
}