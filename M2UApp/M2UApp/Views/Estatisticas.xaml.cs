using M2UApp.Models;
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
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Estatisticas : ContentPage
    {
        private string username;
        private string armazem;

        public Estatisticas()
        {
            InitializeComponent();
        }

        protected async override void OnAppearing()
        {
            textUser.Text = (string)Application.Current.Properties["userLogin"];
            textoArmazem.Text = (string)Application.Current.Properties["ArmazemAtual"];

            username = textUser.Text;
            armazem = textoArmazem.Text;

            ListPicagens.ItemsSource = await RefreshDataAsyncTotal(username, armazem);
            ListPicagensDias.ItemsSource = await RefreshDataAsyncDias(username, armazem);
        }
         
        public async Task<List<Estatistica>> RefreshDataAsyncTotal(string user, string armazem)
        {
            List<Estatistica> estatisticas = new List<Estatistica>();
            HttpClient client = new HttpClient();
            Uri uri = new Uri("http://150.1.101.6:7000/api/estatisticas/total?userUsername=" + user +"&armazemNome=" + armazem );
            HttpResponseMessage responseMessage = await client.GetAsync(uri);

            if (responseMessage.IsSuccessStatusCode)
            {
                string content = await responseMessage.Content.ReadAsStringAsync();
                var Acais = JsonSerializer.Deserialize<List<Estatistica>>(content);

                estatisticas = new List<Estatistica>(Acais);
            }
            return estatisticas;
        }

        public async Task<List<Estatistica>> RefreshDataAsyncDias(string user, string armazem)
        {
            List<Estatistica> estatisticas = new List<Estatistica>();
            HttpClient client = new HttpClient();
            Uri uri = new Uri("http://150.1.101.6:7000/api/estatisticas/dias?userUsername=" + user + "&armazemNome=" + armazem);
            HttpResponseMessage responseMessage = await client.GetAsync(uri);

            if (responseMessage.IsSuccessStatusCode)
            {
                string content = await responseMessage.Content.ReadAsStringAsync();
                var Acais = JsonSerializer.Deserialize<List<Estatistica>>(content);

                estatisticas = new List<Estatistica>(Acais);
            }
            return estatisticas;
        }
    }
}