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
    public partial class TabbedExec : TabbedPage
    {

        public string valCais;
        public TabbedExec()
        {
            InitializeComponent();

        }

        private async void Estado_tarefa_Clicked(object sender, EventArgs e)
        {

            if(valCais != null)
            {
                await Shell.Current.GoToAsync(nameof(ListArtigosExecucao));
            }
            else
            {
                await App.Current.MainPage.DisplayAlert("Erro", "Nenhum Cais Selecionado", "OK");
            }
        }

      /*  public void Cias(object sender, string e)
        {
            valCais = e;

        }*/
    }
}