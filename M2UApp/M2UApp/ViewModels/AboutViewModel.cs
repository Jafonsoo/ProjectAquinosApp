using System;
using System.Threading.Tasks;
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
            GoBackCommand = new Command(async () => await GoBack());

        }

        public Command GoBackCommand { get; set; }
        public ICommand OpenWebCommand { get; }

        private async Task GoBack()
        {
            var result = await Shell.Current.DisplayAlert(
                "Going Back?",
                "Are you sure you want to go back?",
                "Yes, Please!", "Nope!");

            if (result)
            {
                await Shell.Current.GoToAsync("..", true);
            }
        }
    }
}