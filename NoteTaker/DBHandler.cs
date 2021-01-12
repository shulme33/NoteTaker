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
            conn = new SqlConnection("server = localhost ;database = NoteTakerApp;Trusted_Connection = yes");
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



        //public DataRow QuerySingleNote(int ID)
        //{
        //    String query = "select ID, Title, PreviewText, MainText from Notes where ID = @ID"; // + ID;
        //    conn.Open();
        //    SqlCommand cmd = new SqlCommand();
        //    cmd.Connection = conn;
        //    cmd.Parameters.Add("@ID", SqlDbType.Int);
        //    cmd.Parameters["@ID"].Value = ID;
        //    cmd.CommandText = query;

        //    DataRow dr = new DataRow();

        //}

        public void SaveItemFromCanvas(int ID, String title, String previewText, String mainText)
        {

            String query = @"update Notes 
                             set Title = @Title, 
                                 PreviewText = @PreviewText, 
                                 MainText = @MainText,
                                 DateLastEdited = GetDate()
                             where ID = @ID";

            conn.Open();
            SqlCommand cmd = new SqlCommand(query, conn);
            cmd.Parameters.Add("@ID", SqlDbType.Int);
            cmd.Parameters.Add("@Title", SqlDbType.NVarChar);
            cmd.Parameters.Add("@PreviewText", SqlDbType.NVarChar);
            cmd.Parameters.Add("@MainText", SqlDbType.NVarChar);

            cmd.Parameters["@ID"].Value = ID;
            cmd.Parameters["@Title"].Value = title;
            cmd.Parameters["@PreviewText"].Value = previewText;
            cmd.Parameters["@MainText"].Value = mainText;
            cmd.ExecuteNonQuery();
            conn.Close();


            //string stmt = "INSERT INTO dbo.Test(id, name) VALUES(@ID, @Name)";

            //SqlCommand cmd = new SqlCommand(smt, _connection);
            //cmd.Parameters.Add("@ID", SqlDbType.Int);
            //cmd.Parameters.Add("@Name", SqlDbType.VarChar, 100);
            //cmd.Parameters["@ID"].Value = i;
            //cmd.Parameters["@Name"].Value = i.ToString();
            //cmd.ExecuteNonQuery();

            //SqlCommand cmd = new SqlCommand();
            //cmd.Connection = conn;
            //conn.Open();
            //cmd.CommandText = query;
            //conn.Close();
        }

        public int SaveNewItemFromCanvas(String title, String previewText, String mainText)
        {
            String query = @"insert into Notes(Title, PreviewText, MainText, DateAdded, DateLastEdited)
                             output Inserted.ID
                             values(@Title, @PreviewText, @MainText, GetDate(), GetDate())";
            conn.Open();
            SqlCommand cmd = new SqlCommand(query, conn);
            cmd.Parameters.Add("@Title", SqlDbType.NVarChar);
            cmd.Parameters.Add("@PreviewText", SqlDbType.NVarChar);
            cmd.Parameters.Add("@MainText", SqlDbType.NVarChar);

            cmd.Parameters["@Title"].Value = title;
            cmd.Parameters["@PreviewText"].Value = previewText;
            cmd.Parameters["@MainText"].Value = mainText;

            int newID = (int)cmd.ExecuteScalar();
            conn.Close();

            return newID;
        }

        public void DeleteNote(int noteID)
        {
            String query = "delete from Notes where ID = @ID";

            conn.Open();
            SqlCommand cmd = new SqlCommand(query, conn);

            cmd.Parameters.Add("@ID", SqlDbType.Int);
            cmd.Parameters["@ID"].Value = noteID;

            cmd.ExecuteNonQuery();

            conn.Close();
        }
    }
}
