using M2UApp.Models;
using Rg.Plugins.Popup.Extensions;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace M2UApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TabbedPrep : TabbedPage
    {
        public TabbedPrep()
        { 
            InitializeComponent();
        }

    /*    private async void Adicionar_Objeto_Clicked(object sender, EventArgs e)
        {

            ZXingView zXingView = new ZXingView("Leia o código de barras do artigo a ADICIONAR", "O Código será lido automaticamente");
            zXingView.BarcodeReaded += ZXingView_BarcodeReaded;


            await Navigation.PushModalAsync(zXingView);
        }

        private async void Subtrair_leitura_Clicked(object sender, EventArgs e)
        {
            ZXingView zXingView = new ZXingView("Leia o código de barras do artigo a REMOVER", "O Código será lido automaticamente");
            zXingView.BarcodeReaded += ZXingView_BarcodeReaded;

            await Navigation.PushModalAsync(zXingView);
        }
        void ZXingView_BarcodeReaded(object sender, string e)
        {
            string[] delimiterChar = { "#", ""  };

            var splitArray = e.Split(delimiterChar, StringSplitOptions.RemoveEmptyEntries).ToList();
            splitArray.RemoveAt(0);
            string combindedString = string.Join("", splitArray);

            ListArtigosPreparacao listArtigos = new ListArtigosPreparacao();
            listArtigos.ReceberCodigo(sender,combindedString);
        }*/

        private async void Estado_tarefa_Clicked(object sender, EventArgs e)
        {
            /*  PopupArtigos popupArtigos = new PopupArtigos();
              await Navigation.PushPopupAsync(popupArtigos);*/
            await Shell.Current.GoToAsync(nameof(ListArtigosPreparacao));
        }
    }
}