using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace NoteTaker
{
    public class NotePreview
    {
        public int ID { get; set; }
        public String title;
        public String previewText;
        public String mainText;
        public MainWindow mainWindow;
        public NoteList noteList;
        public Button mainButton;
        public TextBlock textTitle;
        public TextBlock textPreview;

        public NotePreview(int ID, String title, String previewText, MainWindow mainWindow, NoteList noteList)
        {
            this.ID = ID;
            this.title = title;
            this.previewText = previewText;
            this.mainWindow = mainWindow;
            this.noteList = noteList;
            ConstructPreviewButton();
        }


        public void ConstructPreviewButton()
        {
            //Generatl Strucutre of each preview
            //
            //< Button >
            //    < StackPanel Orientation = "Vertical" >
            //        < TextBlock FontSize = "20" FontWeight = "Bold" > Item </ TextBlock >
            //        < TextBlock FontSize = "16" TextWrapping = "Wrap" >
            //       this is a very long text inside a textblock and this needs to be on multiline...
            //        </ TextBlock >
            //    </ StackPanel >
            //</ Button >

            //Title Text
            this.textTitle = new TextBlock();
            this.textTitle.FontSize = 20;
            //this.textTitle.FontWeight = FontWeights.Bold;
            this.textTitle.Text = title;
            this.textTitle.Margin = new Thickness(3, 3, 0, 5);
            //this.textTitle.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF0000"));


            //Preview Text
            this.textPreview = new TextBlock();
            this.textPreview.FontSize = 16;
            this.textPreview.TextWrapping = TextWrapping.Wrap;
            this.textPreview.Margin = new Thickness(3, 0, 0, 0);
            this.textPreview.Text = AddElipses(previewText);

            //Stack Panel
            StackPanel stackPanel = new StackPanel();
            stackPanel.Orientation = Orientation.Vertical;
            stackPanel.Children.Add(this.textTitle);
            stackPanel.Children.Add(this.textPreview);

            //Overall Parent Button - Represents each preview
            this.mainButton = new Button();
            this.mainButton.Content = title;
            this.mainButton.HorizontalContentAlignment = HorizontalAlignment.Left;
            this.mainButton.VerticalContentAlignment = VerticalAlignment.Top;
            this.mainButton.Click += new RoutedEventHandler(previewSelected);
            this.mainButton.BorderThickness = new Thickness(0, 0, 0, 1);
            this.mainButton.Content = stackPanel;
            this.mainButton.Padding = new Thickness(5);
            this.mainButton.Height = 102;
            this.mainButton.Cursor = Cursors.Hand;

            //Add to window
            mainWindow.NoteList.Children.Insert(0, this.mainButton);
        }

        public void UpdateItem(String title, String previewText)
        {
            Console.WriteLine("Updating: " + mainButton.ActualHeight);
            this.textTitle.Text = title;
            this.textPreview.Text = AddElipses(previewText);
            
        }

        public void RemoveFromNoteList()
        {
            this.mainWindow.NoteList.Children.Remove(this.mainButton);
        }

        public void previewSelected(object sender, RoutedEventArgs e)
        {
            Console.WriteLine("Clicked 2: " + title);
            noteList.OpenPreviewOnCanvas(this);
        }

        public String AddElipses(String prev)
        {
            if(prev.Length > SystemConstants.PREVIEW_LENGTH){
                return prev.Substring(0, SystemConstants.PREVIEW_LENGTH) + " ..."; 
            }
            return prev;
        }

    }
}
