namespace Pikachu
{
    partial class LoginFormcs
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;
        private Button btnChoi;
        private TextBox tbDangNhap;
        private Label txtTenNguoiChoi;

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
            btnChoi = new Button();
            tbDangNhap = new TextBox();
            txtTenNguoiChoi = new Label();
            btnDangKy = new Button();
            SuspendLayout();
            // 
            // btnChoi
            // 
            btnChoi.Location = new Point(149, 129);
            btnChoi.Name = "btnChoi";
            btnChoi.Size = new Size(96, 37);
            btnChoi.TabIndex = 10;
            btnChoi.Text = "Chơi";
            btnChoi.UseVisualStyleBackColor = true;
            btnChoi.Click += btnChoi_Click;
            // 
            // tbDangNhap
            // 
            tbDangNhap.Location = new Point(36, 76);
            tbDangNhap.MaxLength = 50;
            tbDangNhap.Multiline = true;
            tbDangNhap.Name = "tbDangNhap";
            tbDangNhap.Size = new Size(467, 47);
            tbDangNhap.TabIndex = 8;
            // 
            // txtTenNguoiChoi
            // 
            txtTenNguoiChoi.AutoSize = true;
            txtTenNguoiChoi.Font = new Font("Segoe UI", 15.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            txtTenNguoiChoi.Location = new Point(107, 43);
            txtTenNguoiChoi.Name = "txtTenNguoiChoi";
            txtTenNguoiChoi.Size = new Size(312, 30);
            txtTenNguoiChoi.TabIndex = 6;
            txtTenNguoiChoi.Text = "Vui Lòng Điền Tên Người Chơi";
            // 
            // btnDangKy
            // 
            btnDangKy.Location = new Point(270, 129);
            btnDangKy.Name = "btnDangKy";
            btnDangKy.Size = new Size(96, 37);
            btnDangKy.TabIndex = 11;
            btnDangKy.Text = "Đăng Ký";
            btnDangKy.UseVisualStyleBackColor = true;
            btnDangKy.Click += btnDangKy_Click;
            // 
            // LoginFormcs
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(533, 268);
            Controls.Add(btnDangKy);
            Controls.Add(btnChoi);
            Controls.Add(tbDangNhap);
            Controls.Add(txtTenNguoiChoi);
            Name = "LoginFormcs";
            Text = "LoginFormcs";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button btnDangKy;
    }
}