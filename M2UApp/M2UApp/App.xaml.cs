using M2UApp.Services;
using M2UApp.Views;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace M2UApp
{
    public partial class App : Application
    {

        public App()
        {
            InitializeComponent();
            DependencyService.Register<MockDataStore>();
            MainPage = new AppShell();
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
