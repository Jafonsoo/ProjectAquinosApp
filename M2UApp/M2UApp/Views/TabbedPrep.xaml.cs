using Rg.Plugins.Popup.Services;
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
    public partial class TabbedPrep : TabbedPage
    {

        public TabbedPrep()
        { 
            InitializeComponent();
            //   NomeArmazem.Text = (string)Application.Current.Properties["ArmazemAtual"];
          //  this.Children.Add(new Page() { Title = "ifj" });
          //  this.Children.Add(new AdicionarPreCarga() { Title = "Vista" });
        
        }

        private async void Adicionar_Objeto_Clicked(object sender, EventArgs e)
        {
            /*     var disp = new DisplayPage();
                 await Navigation.PushModalAsync(disp);*/
            var page = new PopupView();
            await PopupNavigation.Instance.PushAsync(page);
        }
    }
}