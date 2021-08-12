using System;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace M2UApp.ViewModels
{
    public class AboutViewModel : BaseViewModel
    {
        string str = "enrjgnjen#2343545";
        string[] delimiterChar = { "\n" };
        
        

        public AboutViewModel()
        {
            Title = "Página Inicial";

            var splitArray = str.Split(delimiterChar, StringSplitOptions.RemoveEmptyEntries).ToString();
            splitArray.Remove(0);
            Console.WriteLine("" + splitArray);
            
            OpenWebCommand = new Command(async () => await Browser.OpenAsync("https://aquinosgroup.com/"));
        }


        public ICommand OpenWebCommand { get; }
    }
}