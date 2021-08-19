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
        public TabbedExec()
        {
            InitializeComponent();

        }

        private async void Subtrair_leitura_Clicked(object sender, EventArgs e)
        {
            ZXingView zXingView = new ZXingView("Leia o código de barras do artigo a remover", "O Código será lido automaticamente");
            zXingView.BarcodeReaded += ZXingView_BarcodeReaded;

            await Navigation.PushModalAsync(zXingView);
        }

        private async void Adicionar_Objeto_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushPopupAsync(new PopupView());
        }

        void ZXingView_BarcodeReaded(object sender, string e)
        {
            //  lblResultado.Text = "QRCODE: " + e;
        }
    }
}