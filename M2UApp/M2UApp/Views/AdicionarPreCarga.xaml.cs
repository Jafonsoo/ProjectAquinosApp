using Rg.Plugins.Popup.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace M2UApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AdicionarPreCarga : ContentPage
    {
        public AdicionarPreCarga()
        {
            InitializeComponent();
            NomeArmazem.Text = (string)Application.Current.Properties["ArmazemAtual"];
        }

    /*    private async void Button_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushPopupAsync(new PopupEncomendas());
        }

        private async void Button_Clicked_1(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync(nameof(ListArtigosPreparacao));
        }*/
    }
}