using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace NoteTaker
{
    public class NotePreview
    {
        public int ID;
        public String title;
        public String previewText;
        public MainWindow mainWindow;
        public Label label;

        public NotePreview(int ID, String title, String previewText, MainWindow mainWindow)
        {
            this.ID = ID;
            this.title = title;
            this.previewText = previewText;
            this.mainWindow = mainWindow;
            ConstructLabel();
        }


        public void ConstructLabel()
        {
            //Title Text
            TextBlock textTitle = new TextBlock();
            textTitle.FontSize = 20;
            textTitle.FontWeight = FontWeights.Bold;
            textTitle.Text = title;

            //Preview Text
            TextBlock textPreview = new TextBlock();
            textPreview.FontSize = 16;
            textPreview.TextWrapping = TextWrapping.Wrap;
            textPreview.Text = previewText;

            //Stack Panel
            StackPanel stackPanel = new StackPanel();
            stackPanel.Orientation = Orientation.Vertical;
            stackPanel.Children.Add(textTitle);
            stackPanel.Children.Add(textPreview);

            //Overall Parent Label
            Label mainLabel = new Label();
            mainLabel.Content = title;
            mainLabel.MouseLeftButtonDown += LabelMouseLeftButtonUp;
            mainLabel.Content = stackPanel;

            //Add to window
            mainWindow.NoteList.Children.Add(mainLabel);
        }

        public void noteClicked(object sender, EventArgs e)
        {

        }

        private void LabelMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            Console.WriteLine("Clicked: " + title);
        }

        //< Label >
        //    < StackPanel Orientation = "Vertical" >
        //        < TextBlock FontSize = "20" FontWeight = "Bold" > Item </ TextBlock >
        //        < TextBlock FontSize = "16" TextWrapping = "Wrap" >
        //       this is a very long text inside a textblock and this needs to be on multiline...
        //        </ TextBlock >
        //    </ StackPanel >
        //</ Label >
    }
}
