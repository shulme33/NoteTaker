using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;

namespace NoteTaker
{
    public class DBHandler
    {

        public SqlConnection conn;

        public DBHandler()
        {
            conn = new SqlConnection("server = localhost ;database = master;Trusted_Connection = yes");
        }

        public DataTable QueryDB(String query)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            conn.Open();
            cmd.CommandText = query;

            //SqlDataReader reader = cmd.ExecuteReader();

            DataTable ds = new DataTable();

            //using(SqlDataReader reader = cmd.ExecuteReader() )
            //{
            //    while (reader.Read())
            //    {
            //        Console.WriteLine(reader);
            //    }
            //}

            using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
            {
                adapter.Fill(ds);
            }

            conn.Close();

            return ds;
        }
    }
}
