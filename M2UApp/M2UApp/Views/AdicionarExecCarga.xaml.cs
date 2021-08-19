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
    public partial class AdicionarExecCarga : ContentPage
    {


        public AdicionarExecCarga()
        {
            InitializeComponent();
            NomeArmazem.Text = (string)Application.Current.Properties["ArmazemAtual"];

          //  MyString = Cais;



        }


        private async void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            PopupCais popupCais = new PopupCais();
            popupCais.CaisReaded += CaisResult;


            await Navigation.PushPopupAsync(popupCais);
        }

        void CaisResult(object sender, string e)
        {
            nomeCais.Text = e;
        }
    }
}