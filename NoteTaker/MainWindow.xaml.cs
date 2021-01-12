using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace NoteTaker
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    


    public partial class MainWindow : Window
    {
        public DBHandler db;
        public NoteList noteList;

        public MainWindow()
        {
            InitializeComponent();

            db = new DBHandler();
            noteList = new NoteList(db, this);
            noteList.LoadNotesList();
        }

        private void ButtonSaveNote_Click(object sender, RoutedEventArgs e)
        {
            noteList.SaveNote();
        }

        private void ButtonAddNote_Click(object sender, RoutedEventArgs e)
        {
            noteList.AddNewNote();
        }

        private void ButtonDeleteNote_Click(object sender, RoutedEventArgs e)
        {
            noteList.DeleteNote();
        }
    }
}
