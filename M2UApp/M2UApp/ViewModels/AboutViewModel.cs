using System;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace M2UApp.ViewModels
{
    public class AboutViewModel : BaseViewModel
    {
        public AboutViewModel()
        {
            Title = "Página Inicial";
            OpenWebCommand = new Command(async () => await Browser.OpenAsync("https://aquinosgroup.com/"));
        }

        public ICommand OpenWebCommand { get; }
    }
}