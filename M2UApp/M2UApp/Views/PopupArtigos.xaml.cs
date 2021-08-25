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
        private List<ArtigosTest> artigosPreparados;
        private int Armazem_ID;
        private string User_Username;
        private string num_encomenda;
         
        public PopupArtigos()
        {
            InitializeComponent();
            armazem_ID.Text = Application.Current.Properties["IdArmazem"].ToString();
            user_Username.Text = (string)Application.Current.Properties["userLogin"];
        } 


        public void ListElementos(IEnumerable<Artigo> artigos, List<ArtigosTest> artigosPrep, string encomenda)
        {
            artigosPreparados = artigosPrep;
            num_encomenda = encomenda;
            User_Username = user_Username.Text;
            Armazem_ID = int.Parse(armazem_ID.Text);

            ListArtigos.ItemsSource = artigos;

        }

        private async void adicionabtn_Clicked(object sender, EventArgs e)
        {
            bool action = await DisplayAlert("", "Pretende enviar a Preparação de Carga?", "Sim", "Não");

            if (action)
            {
            await SaveArtigos();
            await App.Current.MainPage.DisplayAlert("Sucesso", "Preparação de Carga Enviada", "OK");
            await PopupNavigation.Instance.PopAsync();
            await Shell.Current.GoToAsync("..");
            }
            else { }

        }

        private async Task SaveArtigos()
        {
            try
            {
                foreach(ArtigosTest artigo in artigosPreparados) { 

                var artigosPre = new ExpedicaoArtigo
                {
                    Referencia_Artigo = artigo.Referencia_Artigo,
                    NumeroSerie = artigo.NumeroSerie,
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