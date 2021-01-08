using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoteTaker
{
    public class NoteList
    {
        public DBHandler db;

        public NoteList(DBHandler db)
        {
            var results = db.QueryDB("select * from Item");

            //using( results){
            //    while (results.Read())
            //    {
            //        Console.WriteLine(results["ItemName"].ToString());
            //    }
            //}

            foreach (DataRow dr in results.Rows)
            {
                Console.WriteLine(dr["ItemName"]);
            }
        }

    }
}
