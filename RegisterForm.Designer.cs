namespace Pikachu
{
    partial class RegisterForm
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
            btnDangKy = new Button();
            tbDangNhap = new TextBox();
            txtTenNguoiChoi = new Label();
            SuspendLayout();
            // 
            // btnDangKy
            // 
            btnDangKy.Location = new Point(208, 117);
            btnDangKy.Name = "btnDangKy";
            btnDangKy.Size = new Size(96, 37);
            btnDangKy.TabIndex = 13;
            btnDangKy.Text = "Đăng Ký";
            btnDangKy.UseVisualStyleBackColor = true;
            btnDangKy.Click += btnDangKy_Click;
            // 
            // tbDangNhap
            // 
            tbDangNhap.Location = new Point(38, 64);
            tbDangNhap.MaxLength = 50;
            tbDangNhap.Multiline = true;
            tbDangNhap.Name = "tbDangNhap";
            tbDangNhap.Size = new Size(467, 47);
            tbDangNhap.TabIndex = 12;
            // 
            // txtTenNguoiChoi
            // 
            txtTenNguoiChoi.AutoSize = true;
            txtTenNguoiChoi.Font = new Font("Segoe UI", 15.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            txtTenNguoiChoi.Location = new Point(93, 31);
            txtTenNguoiChoi.Name = "txtTenNguoiChoi";
            txtTenNguoiChoi.Size = new Size(349, 30);
            txtTenNguoiChoi.TabIndex = 11;
            txtTenNguoiChoi.Text = "Vui Lòng Đăng Ký Tên Người Chơi";
            // 
            // RegisterForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(549, 280);
            Controls.Add(btnDangKy);
            Controls.Add(tbDangNhap);
            Controls.Add(txtTenNguoiChoi);
            Name = "RegisterForm";
            Text = "RegisterForm";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button btnDangKy;
        private TextBox tbDangNhap;
        private Label txtTenNguoiChoi;
    }
}