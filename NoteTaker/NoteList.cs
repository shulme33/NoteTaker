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
        public NotePreview curNoteOnCanvas;

        public NoteList(DBHandler db, MainWindow mainWindow)
        {
            this.db = db;
            this.mainWindow = mainWindow;
            this.previews = new List<NotePreview>();
            this.curNoteOnCanvas = null;
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

            this.curNoteOnCanvas = previews[0];
            OpenPreviewOnCanvas(this.curNoteOnCanvas);
        }

        public void DeleteNote()
        {
            db.DeleteNote(this.curNoteOnCanvas.ID);
            int curIndex = previews.IndexOf(this.curNoteOnCanvas);
            previews.Remove(this.curNoteOnCanvas);
            this.curNoteOnCanvas.DeleteItem();

            if (previews.Count > 0 && curIndex < previews.Count)
            {
                this.curNoteOnCanvas = previews[curIndex];
            } else if (previews.Count > 0 && curIndex >= previews.Count)
            {
                this.curNoteOnCanvas = previews[previews.Count-1];
            }
            else
            {
                AddNewNote();
            }

            OpenPreviewOnCanvas(this.curNoteOnCanvas);
        }

        public void AddNewNote()
        {
            String newTitle = "New Note";
            String newMainText = "test";
            NotePreview newNote = new NotePreview(-1,
                                                  newTitle,
                                                  newMainText,
                                                  mainWindow,
                                                  this);
            previews.Add(newNote);
            OpenNewPreviewOnCanvas(newNote, newTitle, newMainText);
        }

        public void SaveNote()
        {
            if (this.curNoteOnCanvas.ID == -1)
            {
                //If -1, then this is a new note and doesn't exist in the system.
                Console.WriteLine("New note being saved");
                this.curNoteOnCanvas.ID = db.SaveNewItemFromCanvas(mainWindow.CanvasTitle.Text,
                                            GetSubstring(mainWindow.CanvasMainText.Text, SystemConstants.PREVIEW_LENGTH),
                                            mainWindow.CanvasMainText.Text);
            }
            else
            {
                Console.WriteLine("Existing note being saved");
                db.SaveItemFromCanvas(this.curNoteOnCanvas.ID,
                                      mainWindow.CanvasTitle.Text,
                                      GetSubstring(mainWindow.CanvasMainText.Text, SystemConstants.PREVIEW_LENGTH),
                                      mainWindow.CanvasMainText.Text);
            }


            var results = db.QueryDB("select ID, Title, PreviewText, MainText from Notes where ID = " + this.curNoteOnCanvas.ID);
            
            DataRow dr = results.Rows[0];
            
            curNoteOnCanvas.UpdateItem(dr["Title"].ToString(), 
                                        dr["PreviewText"].ToString());
        }

        public void OpenNewPreviewOnCanvas(NotePreview note, String newTitle, String newMainText)
        {
            mainWindow.CanvasTitle.Text = newTitle;
            mainWindow.CanvasMainText.Text = newMainText;
            this.curNoteOnCanvas = note;
        }
        
        public void OpenPreviewOnCanvas(NotePreview note)
        {
            if (this.curNoteOnCanvas.ID == -1)
            {
                return;
            }
            Console.Write("Setting Canvas");
            //Set main canvas data
            var results = db.QueryDB("select Title, MainText from Notes where ID = " + note.ID);

            mainWindow.CanvasTitle.Text = results.Rows[0]["Title"].ToString();
            mainWindow.CanvasMainText.Text = results.Rows[0]["MainText"].ToString();
            this.curNoteOnCanvas = note;
        }

        public String GetSubstring(String str, int len)
        {
            if (str.Length > len)
            {
                return str.Substring(0, len);
            }
            return str;
        }
    }
}
