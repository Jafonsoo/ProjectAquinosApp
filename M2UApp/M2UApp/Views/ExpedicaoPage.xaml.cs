using M2UApp.Helpers;
using M2UApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace M2UApp.Views
{
    public partial class ExpedicaoPage : ContentPage
    { 
        public string Entry
        { 
            set
            {
           //     textLabel2.Text = Settings.Login;
            }
             
        }

        public ExpedicaoPage()
        {
            InitializeComponent();
        } 

        private async void TapGestureRecognizer_Preparacao(object sender, EventArgs e)
        {

            await Shell.Current.GoToAsync(nameof(PreparacaoPage));
        }

        private void TapGestureRecognizer_Execucao(object sender, EventArgs e)
        {
            App.Current.MainPage.DisplayAlert("Erro", "Ex de carga", "OK");
        }
    }
}