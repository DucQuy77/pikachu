namespace Pikachu
{
    partial class FormBXH
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
            DataGridViewCellStyle dataGridViewCellStyle1 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle2 = new DataGridViewCellStyle();
            dataGVLeaderBoard = new DataGridView();
            lblTieuDeBXH = new Label();
            ((System.ComponentModel.ISupportInitialize)dataGVLeaderBoard).BeginInit();
            SuspendLayout();
            // 
            // dataGVLeaderBoard
            // 
            dataGVLeaderBoard.AllowUserToAddRows = false;
            dataGVLeaderBoard.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGVLeaderBoard.BackgroundColor = SystemColors.ButtonFace;
            dataGVLeaderBoard.BorderStyle = BorderStyle.None;
            dataGridViewCellStyle1.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = SystemColors.Control;
            dataGridViewCellStyle1.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            dataGridViewCellStyle1.ForeColor = SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = DataGridViewTriState.True;
            dataGVLeaderBoard.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            dataGVLeaderBoard.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = SystemColors.Window;
            dataGridViewCellStyle2.Font = new Font("Segoe UI", 14.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            dataGridViewCellStyle2.ForeColor = SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = DataGridViewTriState.False;
            dataGVLeaderBoard.DefaultCellStyle = dataGridViewCellStyle2;
            dataGVLeaderBoard.Location = new Point(12, 37);
            dataGVLeaderBoard.Name = "dataGVLeaderBoard";
            dataGVLeaderBoard.ReadOnly = true;
            dataGVLeaderBoard.Size = new Size(776, 495);
            dataGVLeaderBoard.TabIndex = 0;
            // 
            // lblTieuDeBXH
            // 
            lblTieuDeBXH.AutoSize = true;
            lblTieuDeBXH.Font = new Font("Segoe UI", 14.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblTieuDeBXH.Location = new Point(323, 9);
            lblTieuDeBXH.Name = "lblTieuDeBXH";
            lblTieuDeBXH.Size = new Size(167, 25);
            lblTieuDeBXH.TabIndex = 1;
            lblTieuDeBXH.Text = "BẢNG XẾP HẠNG";
            // 
            // FormBXH
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 543);
            Controls.Add(lblTieuDeBXH);
            Controls.Add(dataGVLeaderBoard);
            Name = "FormBXH";
            Text = "FormBXH";
            Load += FormBXH_Load;
            ((System.ComponentModel.ISupportInitialize)dataGVLeaderBoard).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private DataGridView dataGVLeaderBoard;
        private Label lblTieuDeBXH;
    }
}