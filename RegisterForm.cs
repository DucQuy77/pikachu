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

namespace Pikachu
{
    public partial class RegisterForm : Form
    {
        public RegisterForm()
        {
            InitializeComponent();
        }

        private void btnDangKy_Click(object sender, EventArgs e)
        {
            string playerName = tbDangNhap.Text.Trim();

            if (string.IsNullOrEmpty(playerName))
            {
                MessageBox.Show("Vui lòng điền đầy đủ thông tin.");
                return;
            }

            using (SqlConnection conn = Connect.GetConnection())
            {
                conn.Open();
                string checkQuery = "SELECT COUNT(*) FROM Information WHERE PlayerName = @playerName";
                SqlCommand checkCmd = new SqlCommand(checkQuery, conn);
                checkCmd.Parameters.AddWithValue("@playerName", playerName);

                int exists = (int)checkCmd.ExecuteScalar();

                if (exists > 0)
                {
                    MessageBox.Show("Tên đã tồn tại!");
                }
                else
                {
                    string insertQuery = "INSERT INTO Information (PlayerName, Score) VALUES (@playerName, 0)";
                    SqlCommand insertCmd = new SqlCommand(insertQuery, conn);
                    insertCmd.Parameters.AddWithValue("@playerName", playerName);
                    insertCmd.ExecuteNonQuery();

                    MessageBox.Show("Đăng ký thành công!");
                    this.Close();
                }
            }
        }
    }
}
