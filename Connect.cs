using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace Pikachu
{
    public class Connect
    {
        private static string connectionString = "Data Source=EDU-NEW-PC\\SQLEXPRESS;Initial Catalog=Pikachu;Integrated Security=True;Encrypt=True;TrustServerCertificate=True";

        [Obsolete]
        public static SqlConnection GetConnection()
        {
            return new SqlConnection(connectionString);
        }

        public DataTable GetData(string query) 
        {
            using (SqlConnection conn = GetConnection())
            {
                conn.Open();
                SqlDataAdapter da = new SqlDataAdapter(query, conn);
                DataTable dt = new DataTable();
                da.Fill(dt);
                return dt;
            }
        }

        public void ExecuteNonQuery(string query) {
            using (SqlConnection conn = GetConnection())
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.ExecuteNonQuery();
            }
        }
    }
}
