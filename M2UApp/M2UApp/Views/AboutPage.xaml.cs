using System;
using System.ComponentModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace M2UApp.Views
{
    [QueryProperty("Entry", "entry")]
    public partial class AboutPage : ContentPage
    {

        public string Entry
        {
            set
            {
                textLabel.Text = Uri.UnescapeDataString(value);
            }

        }
        public AboutPage()
        {
            InitializeComponent();
        }
    }
}