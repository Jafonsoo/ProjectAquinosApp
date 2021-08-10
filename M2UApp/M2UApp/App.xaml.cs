using M2UApp.Services;
using M2UApp.Views;
using System;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace M2UApp
{
    public partial class App : Application
    {

        public App()
        {
            InitializeComponent();
            Connectivity.ConnectivityChanged += ConnectivityChanged;
            DependencyService.Register<MockDataStore>();
            MainPage = new AppShell();
        }

        private void ConnectivityChanged(object sender, ConnectivityChangedEventArgs e)
        {
            if (e.NetworkAccess != NetworkAccess.Internet)
            {
                Application.Current.MainPage.DisplayAlert("Atenção", "Não á ligação á internet", "OK");
            }

        }

        private void InitializeDi()
        {

        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
