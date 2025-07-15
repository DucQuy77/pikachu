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
    public partial class FormBXH : Form
    {
        public FormBXH()
        {
            InitializeComponent();
        }

        private void FormBXH_Load(object sender, EventArgs e)
        {
            using (SqlConnection conn = Connect.GetConnection())
            {
                conn.Open();
                string query = "SELECT PlayerName, Score FROM Information ORDER BY Score DESC";

                SqlDataAdapter adapter = new SqlDataAdapter(query, conn);
                DataTable dt = new DataTable();
                adapter.Fill(dt);

                dataGVLeaderBoard.DataSource = dt;
            }
        }
    }
}
