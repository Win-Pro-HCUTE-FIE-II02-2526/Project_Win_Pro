namespace bt1
{
    partial class f_Forget
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
            this.txb_Email = new Guna.UI2.WinForms.Guna2TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btn_Accept = new Guna.UI2.WinForms.Guna2Button();
            this.label2 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // txb_Email
            // 
            this.txb_Email.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txb_Email.DefaultText = "Email khôi phục";
            this.txb_Email.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.txb_Email.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.txb_Email.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txb_Email.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txb_Email.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txb_Email.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txb_Email.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txb_Email.Location = new System.Drawing.Point(247, 174);
            this.txb_Email.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txb_Email.Name = "txb_Email";
            this.txb_Email.PlaceholderText = "";
            this.txb_Email.SelectedText = "";
            this.txb_Email.Size = new System.Drawing.Size(326, 48);
            this.txb_Email.TabIndex = 0;
            this.txb_Email.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txb_Email.TextChanged += new System.EventHandler(this.txb_Email_TextChanged);
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.White;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Black;
            this.label1.Location = new System.Drawing.Point(101, 79);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(611, 68);
            this.label1.TabIndex = 1;
            this.label1.Text = "Vui lòng nhập địa chỉ email của bạn để bắt đầu khôi phục mật khẩu";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // btn_Accept
            // 
            this.btn_Accept.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btn_Accept.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btn_Accept.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btn_Accept.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btn_Accept.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Accept.ForeColor = System.Drawing.Color.White;
            this.btn_Accept.Location = new System.Drawing.Point(247, 242);
            this.btn_Accept.Name = "btn_Accept";
            this.btn_Accept.Size = new System.Drawing.Size(326, 48);
            this.btn_Accept.TabIndex = 2;
            this.btn_Accept.Text = "Tiếp tục";
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.Color.White;
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(98, 79);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(629, 273);
            this.label2.TabIndex = 3;
            // 
            // f_Forget
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::bt1.Properties.Resources.ảnh_trường;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.btn_Accept);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txb_Email);
            this.Controls.Add(this.label2);
            this.Name = "f_Forget";
            this.Text = "f_Forget";
            this.ResumeLayout(false);

        }

        #endregion

        private Guna.UI2.WinForms.Guna2TextBox txb_Email;
        private System.Windows.Forms.Label label1;
        private Guna.UI2.WinForms.Guna2Button btn_Accept;
        private System.Windows.Forms.Label label2;
    }
}