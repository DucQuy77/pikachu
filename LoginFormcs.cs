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
    public partial class LoginFormcs : Form
    {
        public string PlayerName { get; private set; }
        public LoginFormcs()
        {
            InitializeComponent();
        }

        private void btnChoi_Click(object sender, EventArgs e)
        {
            string playerName = tbDangNhap.Text.Trim();

            using (SqlConnection conn = Connect.GetConnection())
            {
                conn.Open();
                string query = "SELECT COUNT(*) FROM Information WHERE PlayerName = @playerName";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@playerName", playerName);

                int count = (int)cmd.ExecuteScalar();

                if (count > 0)
                {
                    PlayerName = playerName;
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Hãy thử lại!");
                }
            }
        }

        private void btnDangKy_Click(object sender, EventArgs e)
        {
            RegisterForm registerForm = new RegisterForm();
            registerForm.ShowDialog();
        }
    }
}
