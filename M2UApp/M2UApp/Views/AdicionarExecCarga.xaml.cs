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
        string cais;
        public AdicionarExecCarga()
        {
            InitializeComponent();
            NomeArmazem.Text = (string)Application.Current.Properties["ArmazemAtual"];
        }


        private async void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            PopupCais popupCais = new PopupCais();
            popupCais.CaisReaded += CaisResult;

            await Navigation.PushPopupAsync(popupCais);
        }

        public void CaisResult(object sender, string e)
        {
            nomeCais.Text = e;
            cais = e;
        }

        private async void Estado_tarefa_Clicked(object sender, EventArgs e)
        {
            if (cais == null)
            {
             await App.Current.MainPage.DisplayAlert("Erro", "Nenhum Cais Selecionado", "OK");
            }
            else {

            bool action = await DisplayAlert("", "Deseja iniciar a picagem no " + cais +"?", "Sim", "Não");

            if(action)
            {
              Application.Current.Properties["CaisAtual"] = cais;
              await Shell.Current.GoToAsync(nameof(ListArtigosExecucao));
            } 
            else 
            { }
            }
        }
    }
}