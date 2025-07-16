namespace Pikachu
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            panelTop = new Panel();
            btnBangXH = new Button();
            lblDiemCao = new Label();
            btnChoiLai = new Button();
            lblTime = new Label();
            lblScore = new Label();
            panelBoard = new Panel();
            panelOverlay = new Panel();
            panelTop.SuspendLayout();
            SuspendLayout();
            // 
            // panelTop
            // 
            panelTop.BackColor = SystemColors.ScrollBar;
            panelTop.Controls.Add(btnBangXH);
            panelTop.Controls.Add(lblDiemCao);
            panelTop.Controls.Add(btnChoiLai);
            panelTop.Controls.Add(lblTime);
            panelTop.Controls.Add(lblScore);
            panelTop.Dock = DockStyle.Top;
            panelTop.Location = new Point(0, 0);
            panelTop.Name = "panelTop";
            panelTop.Size = new Size(886, 44);
            panelTop.TabIndex = 0;
            // 
            // btnBangXH
            // 
            btnBangXH.BackColor = SystemColors.AppWorkspace;
            btnBangXH.Font = new Font("Segoe UI", 14.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnBangXH.Location = new Point(621, 5);
            btnBangXH.Name = "btnBangXH";
            btnBangXH.Size = new Size(113, 32);
            btnBangXH.TabIndex = 5;
            btnBangXH.Text = "BXH";
            btnBangXH.UseVisualStyleBackColor = false;
            btnBangXH.Click += btnBangXH_Click;
            // 
            // lblDiemCao
            // 
            lblDiemCao.AutoSize = true;
            lblDiemCao.BackColor = SystemColors.MenuText;
            lblDiemCao.Font = new Font("Segoe UI", 14.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblDiemCao.ForeColor = Color.Honeydew;
            lblDiemCao.Location = new Point(12, 10);
            lblDiemCao.Name = "lblDiemCao";
            lblDiemCao.Size = new Size(136, 25);
            lblDiemCao.TabIndex = 3;
            lblDiemCao.Text = "Điểm kỷ lục: 0";
            // 
            // btnChoiLai
            // 
            btnChoiLai.BackColor = SystemColors.AppWorkspace;
            btnChoiLai.Font = new Font("Segoe UI", 14.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnChoiLai.Location = new Point(740, 5);
            btnChoiLai.Name = "btnChoiLai";
            btnChoiLai.Size = new Size(113, 32);
            btnChoiLai.TabIndex = 2;
            btnChoiLai.Text = "Chơi lại";
            btnChoiLai.UseVisualStyleBackColor = false;
            btnChoiLai.Click += btnChoiLai_Click;
            // 
            // lblTime
            // 
            lblTime.AutoSize = true;
            lblTime.Font = new Font("Segoe UI", 14.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblTime.ForeColor = Color.IndianRed;
            lblTime.Location = new Point(314, 10);
            lblTime.Name = "lblTime";
            lblTime.Size = new Size(135, 25);
            lblTime.TabIndex = 1;
            lblTime.Text = "Thời gian: 60s";
            // 
            // lblScore
            // 
            lblScore.AutoSize = true;
            lblScore.Font = new Font("Segoe UI", 14.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblScore.Location = new Point(194, 10);
            lblScore.Name = "lblScore";
            lblScore.Size = new Size(79, 25);
            lblScore.TabIndex = 0;
            lblScore.Text = "Điểm: 0";
            // 
            // panelBoard
            // 
            panelBoard.BackColor = Color.White;
            panelBoard.BorderStyle = BorderStyle.FixedSingle;
            panelBoard.Location = new Point(12, 50);
            panelBoard.Name = "panelBoard";
            panelBoard.Size = new Size(862, 488);
            panelBoard.TabIndex = 1;
            panelBoard.Paint += panelBoard_Paint;
            // 
            // panelOverlay
            // 
            panelOverlay.Location = new Point(0, 53);
            panelOverlay.Name = "panelOverlay";
            panelOverlay.Size = new Size(8, 8);
            panelOverlay.TabIndex = 0;
            panelOverlay.Paint += panelOverlay_Paint;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(886, 550);
            Controls.Add(panelOverlay);
            Controls.Add(panelBoard);
            Controls.Add(panelTop);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            Name = "Form1";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Pikachu";
            FormClosing += Form1_FormClosing;
            panelTop.ResumeLayout(false);
            panelTop.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Panel panelTop;
        private Label lblScore;
        private Button btnChoiLai;
        private Label lblTime;
        private Panel panelBoard;
        private Label lblDiemCao;
        private Button btnBangXH;
        private Panel panelOverlay;
    }
}
