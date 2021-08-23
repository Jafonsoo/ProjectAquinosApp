using M2UApp.Models;
using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace M2UApp.Views
{
    public partial class PopupArtigos : PopupPage
    {
        List<Artigo> artigosPreparados;
        int Armazem_ID;
        string User_Username;
        string num_encomenda;
         
        public PopupArtigos()
        {
            InitializeComponent();
            AAasdfghjk.Text = Application.Current.Properties["IdArmazem"].ToString();
            BBasdfghjkl.Text = (string)Application.Current.Properties["userLogin"];
        } 


        public void ListElementos(IEnumerable<Artigo> artigos, List<Artigo> artigosPrep, string encomenda)
        {
            artigosPreparados = artigosPrep;
            num_encomenda = encomenda;
            User_Username = BBasdfghjkl.Text;
            Armazem_ID = int.Parse(AAasdfghjk.Text);

            ListArtigos.ItemsSource = artigos;

        }

        private async void adicionabtn_Clicked(object sender, EventArgs e)
        {
            bool action = await DisplayAlert("", "Pretende Guardar a Preparação de Carga?", "Sim", "Não");

            if (action)
            {
            await SaveArtigos();
            await App.Current.MainPage.DisplayAlert("Sucesso", "Preparação de Carga Registada", "OK");
            await PopupNavigation.Instance.PopAsync();
            await Shell.Current.GoToAsync($"//{nameof(AboutPage)}");
            }
            else {}

        }

        private async Task SaveArtigos()
        {
            try
            {
                foreach(Artigo artigo in artigosPreparados) { 

                var artigosPre = new ExpedicaoArtigo
                {
                    Referencia_Artigo = artigo.Referencia_Artigo,
                    Quantidade = artigo.Quantidade,
                    QuantidadePicado = artigo.QuantidadePicado,
                    NumeroEncomenda = num_encomenda,
                    Armazem_ID = Armazem_ID,
                    User_Username = User_Username

                };

                await AddArtigosPreparados(artigosPre);

                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public async Task AddArtigosPreparados(ExpedicaoArtigo preparados)
        {
            HttpClient client = new HttpClient();

            Uri uri = new Uri("http://150.1.101.6:7000/api/encomendas/artigos");
            HttpResponseMessage response = await client.PostAsync(uri, new StringContent(JsonSerializer.Serialize(preparados), Encoding.UTF8, "application/json"));

            response.EnsureSuccessStatusCode();

        }

        private async void cancelarbtn_Clicked(object sender, EventArgs e)
        {
            await PopupNavigation.Instance.PopAsync();
        }
    }
}