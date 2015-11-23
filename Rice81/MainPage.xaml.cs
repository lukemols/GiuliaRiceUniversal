using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using Rice81.Resources;
using GiuliaRiceUniversal.Classes;

namespace Rice81
{
    public partial class MainPage : PhoneApplicationPage
    {
        delegate void NewGameDelegate(string s);
        // Constructor
        public MainPage()
        {
            InitializeComponent();
            ApplicationBar.IsVisible = false;

            ProgramManager.Initialize();
            List<string> fileList = ProgramManager.GetFilesList();
            FileList.ItemsSource = fileList;

            List<string> exprList = new List<string>();
            string[] e = new string[] { "Verb", "Vocabulary", "Grammar", "Idiomatic", "Multiple", "Paraphrasing", "Phrasal" };
            foreach (string s in e)
                exprList.Add(s);
            MultiSelect.IsSelectionEnabled = true;
     
            //MultiSelect.ItemsSource = exprList;
        }

        private void FileList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (FileList.SelectedItem == null)
                return;

            List<string> expr = new List<string>();

            foreach(CheckBox c in  MultiSelect.Items)
            {
                if(c.IsChecked == true)
                   expr.Add(c.Content.ToString());
            }

            ProgramManager.LoadFile((string)FileList.SelectedItem, expr);
            ApplicationBar.IsVisible = true;
        }

        private void ApplicationBarIconButton_Click(object sender, EventArgs e)
        {
            if (ProgramManager.State == ProgramManager.Status.LOADED)
                NavigationService.Navigate(new Uri("/GamePage.xaml", UriKind.Relative));
        }
    }
}