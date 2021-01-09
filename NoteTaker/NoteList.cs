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
        public List<NotePreview> previews;
        public int curNoteOnCanvas;

        public NoteList(DBHandler db, MainWindow mainWindow)
        {
            this.db = db;
            this.mainWindow = mainWindow;
            this.previews = new List<NotePreview>();
            this.curNoteOnCanvas = -1;
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
                                                        mainWindow,
                                                        this);
                previews.Add(newNote);

            }

            this.curNoteOnCanvas = previews[0].ID; ;
            OpenPreviewOnCanvas(this.curNoteOnCanvas);
        }

        public void SaveNote()
        {
            Console.WriteLine("Note being saved");
            db.SaveItemFromCanvas(this.curNoteOnCanvas, 
                                  mainWindow.CanvasTitle.Text,
                                  mainWindow.CanvasMainText.Text.Substring(0, 35), 
                                  mainWindow.CanvasMainText.Text);
        }

        public void OpenPreviewOnCanvas(int noteID)
        {
            Console.Write("Setting Canvas");
            //Set main canvas data
            var results = db.QueryDB("select Title, MainText from Notes where ID = " + noteID);

            mainWindow.CanvasTitle.Text = results.Rows[0]["Title"].ToString();
            mainWindow.CanvasMainText.Text = results.Rows[0]["MainText"].ToString();
        }
    }
}
