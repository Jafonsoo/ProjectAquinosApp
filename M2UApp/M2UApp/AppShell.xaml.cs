using M2UApp.ViewModels;
using M2UApp.Views;
using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace M2UApp
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();

            Routing.RegisterRoute(nameof(LoginPage), typeof(LoginPage));
            Routing.RegisterRoute(nameof(ItemDetailPage), typeof(ItemDetailPage));
            Routing.RegisterRoute(nameof(NewItemPage), typeof(NewItemPage));
            
        }

        private async void OnMenuItemClicked(object sender, EventArgs e)
        {
            var action = await DisplayAlert("Sair", "Pretende Terminar a sessão?", "Sim", "Não");

            if (action) 
            { 

            await Current.GoToAsync($"//{nameof(LoginPage)}");

            }else
            {

            }
        }
    }
}
