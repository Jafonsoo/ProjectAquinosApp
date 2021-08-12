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
    public partial class InternosPage : ContentPage
    {
        public InternosPage()
        {
            InitializeComponent();
        }

        private void TapGestureRecognizer_Contagem(object sender, EventArgs e)
        {
            App.Current.MainPage.DisplayAlert("Erro", "Contagem", "OK");
        }

        private void TapGestureRecognizer_Rec(object sender, EventArgs e)
        {
            App.Current.MainPage.DisplayAlert("Erro", "Rec", "OK");
        }

        private void TapGestureRecognizer_Trsf(object sender, EventArgs e)
        {
            App.Current.MainPage.DisplayAlert("Erro", "Transf", "OK");
        }
    }
}