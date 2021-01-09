using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoteTaker
{
    class DBQueries
    {
        public static String SaveCanvasNote(int ID, String title, String previewText, String mainText)
        {
            //update Notes
            //set Title = 'Ninth Note',
            //    PreviewText = 'Now we are on note numero does. This',
            //    MainText = 'NOEW TESTER Now we are on note numero does. This had better work or else I am going to lose my cool.'
            //    where ID = 9

            String query = "update Notes set Title = " + title + ",";
            query += "PreviewText = " + previewText + ",";
            query += "MainText = " + mainText + ",";
            query += "where ID = " + ID;

            return query;
        }
    }
}
