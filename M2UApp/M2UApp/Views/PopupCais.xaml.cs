using M2UApp.Models;
using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace M2UApp.Views
{
    public partial class PopupCais : PopupPage
    {
        public List<Cais> cais;
        public Cais cais2;
        public event EventHandler<string> CaisReaded;

        public PopupCais()
        {
            InitializeComponent();

            adicionabtn.Clicked += async (sender, e) =>
            {
                if (cais2 != null)
                {
                    var result = cais2.NomeCais;

                    Application.Current.Properties["CaisAtual"] = cais2.NomeCais;
                    CaisReaded?.Invoke(this, result.ToString());

                    await PopupNavigation.Instance.PopAsync();
                }
            };

        }
         
        protected async override void OnAppearing() 
        {
            ListCais.ItemsSource = await RefreshDataAsync((int)Application.Current.Properties["IdArmazem"]);
        }

        public async Task<List<Cais>> RefreshDataAsync(int idArmazem)
        {
            cais = new List<Cais>();
            HttpClient client = new HttpClient();
            Uri uri = new Uri("http://150.1.101.6:7000/api/cais/cais?idArmazem=" + idArmazem);
            HttpResponseMessage responseMessage = await client.GetAsync(uri);

            if (responseMessage.IsSuccessStatusCode)
            {
                string content = await responseMessage.Content.ReadAsStringAsync();
                var Acais = JsonSerializer.Deserialize<List<Cais>>(content);

                cais = new List<Cais>(Acais);
            }
            return cais;
        }

        private void ListCais_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var caisSelecionado = e.SelectedItem as Cais;
            if(caisSelecionado != null)
            {
                cais2 = caisSelecionado;
            }
        }

        private async void cancelarbtn_Clicked(object sender, EventArgs e)
        {
            await PopupNavigation.Instance.PopAsync();
        }

    }
}