
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
            Routing.RegisterRoute(nameof(TestQR), typeof(TestQR));
            Routing.RegisterRoute(nameof(Definicoes), typeof(Definicoes));
            Routing.RegisterRoute(nameof(TESTE), typeof(TESTE));
            Routing.RegisterRoute(nameof(AboutPage), typeof(AboutPage));
            Routing.RegisterRoute(nameof(ArmazensPage), typeof(ArmazensPage));

            Routing.RegisterRoute(nameof(ItemDetailPage), typeof(ItemDetailPage));
            Routing.RegisterRoute(nameof(NewItemPage), typeof(NewItemPage));


            
        }

        private async void OnMenuItemClicked(object sender, EventArgs e)
        {
            bool action = await DisplayAlert("", "Pretende Terminar a sessão?", "Sim", "Não");

            if (action)
            {
                await Shell.Current.GoToAsync($"//LoginPage");
            }
            else { }
        }


    }
}
