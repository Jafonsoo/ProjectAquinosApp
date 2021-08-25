using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using M2UApp.Models;
using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Services;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace M2UApp.Views
{
    public partial class PopupEncomendas : PopupPage
    {
        public event EventHandler<string> EncomendaReaded;
        public ListArtigosPreparacao artigosPreparacao;

        public PopupEncomendas()
        {
            InitializeComponent();
        }

        private async void adicionabtn_Clicked(object sender, EventArgs e)
        {
            string value = codigoEncomenda.Text;

            EncomendaReaded?.Invoke(this, value);
            await PopupNavigation.Instance.PopAsync();
            
        }
    }
}