using System;
using System.Collections;
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
        public ArrayList previews;

        public NoteList(DBHandler db, MainWindow mainWindow)
        {
            this.db = db;
            this.mainWindow = mainWindow;
            this.previews = new ArrayList();
        }

        public void LoadNotesList()
        {
            var results = db.QueryDB("select * from Notes");

            foreach (DataRow dr in results.Rows)
            {
                Console.WriteLine(dr["Title"]);

                NotePreview newNote = new NotePreview((int)dr["ID"], 
                                                        dr["Title"].ToString(), 
                                                        dr["MainText"].ToString(), 
                                                        mainWindow);
                previews.Add(newNote);

            }
        }

    }
}
