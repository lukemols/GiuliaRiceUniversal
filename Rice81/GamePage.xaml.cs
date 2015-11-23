using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using GiuliaRiceUniversal.Classes;

namespace Rice81
{
    public partial class Page1 : PhoneApplicationPage
    {
        Espression actualExpression;
        public Page1()
        {
            InitializeComponent();
            NewExpression();
        }
        
        public void NewExpression()
        {
            try
            {
                actualExpression = ProgramManager.NextExpression();
            }
            catch(ArgumentNullException)
            {
                NavigationService.Navigate(new Uri("/MainPage.xaml", UriKind.Relative));
            }
            EnglishBlock.Text = actualExpression.Eng;
            if (ShowIta.IsChecked.Value)
                ItalianBlock.Text = actualExpression.Ita;
            else
                ItalianBlock.Text = "Per mostrare questa espressione, sposta l'interruttore a destra";
            RemainingBlock.Text = "Espressioni rimaste: " + ProgramManager.ExpressionsLeft;
        }

        private void ApplicationBarIconButton_Click(object sender, EventArgs e)
        {
            ShowIta.IsChecked = false;
            NewExpression();
        }

        private void ShowIta_Click(object sender, RoutedEventArgs e)
        {
            if (ShowIta.IsChecked.Value)
                ItalianBlock.Text = actualExpression.Ita;
            else
                ItalianBlock.Text = "Per mostrare questa espressione, sposta l'interruttore a destra";
        }
    }
}