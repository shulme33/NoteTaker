using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace NoteTaker
{
    public class NoteList
    {
        public DBHandler db;
        public MainWindow mainWindow;

        public NoteList(DBHandler db, MainWindow mainWindow)
        {
            this.db = db;
            this.mainWindow = mainWindow;
        }

        public void LoadNotesList()
        {
            var results = db.QueryDB("select * from Notes");

            foreach (DataRow dr in results.Rows)
            {
                Console.WriteLine(dr["Title"]);
                Label txt = new Label();
                txt.Content = dr["title"].ToString();

                mainWindow.NoteList.Children.Add(txt);
                //Grid.SetRow(txt, 1);
                //Grid.SetColumn(txt, 1);
            }
        }

    }
}
