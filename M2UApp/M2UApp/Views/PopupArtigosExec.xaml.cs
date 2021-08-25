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
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PopupArtigosExec : PopupPage
    {
        private List<ArtigosTest> artigosPreparados;
        private string Armazem_Nome;
        private string User_Username;
        private string num_encomenda;
        private string Cais_nome;

        public PopupArtigosExec()
        {
            InitializeComponent();
            armazem_ID.Text = (string)Application.Current.Properties["ArmazemAtual"];
            user_Username.Text = (string)Application.Current.Properties["userLogin"];
            cais_name.Text = (string)Application.Current.Properties["CaisAtual"];
        }

        public void ListElementos(IEnumerable<ExpedicaoArtigo> artigos, List<ArtigosTest> artigosPrep, string encomenda)
        {
            artigosPreparados = artigosPrep;
            num_encomenda = encomenda;
            User_Username = user_Username.Text;
            Armazem_Nome = armazem_ID.Text;
            Cais_nome = cais_name.Text;

            ListArtigos.ItemsSource = artigos;

        }

        private async void adicionabtn_Clicked(object sender, EventArgs e)
        {
            bool action = await DisplayAlert("", "Pretende enviar a Execução de Carga?", "Sim", "Não");

            if (action)
            {
                await SaveArtigos();
                await App.Current.MainPage.DisplayAlert("Sucesso", "Execução de Carga Enviada", "OK");
                await PopupNavigation.Instance.PopAsync();
                await Shell.Current.GoToAsync("..");
            }
            else { }
        }

        private async Task SaveArtigos()
        {
            try
            {
                foreach (ArtigosTest artigo in artigosPreparados)
                {

                    var artigosPre = new ExpedicaoArtigo
                    {
                        Referencia_Artigo = artigo.Referencia_Artigo,
                        NumeroSerie = artigo.NumeroSerie,
                        NumeroEncomenda = num_encomenda,
                        Armazem_Nome = Armazem_Nome,
                        User_Username = User_Username,
                        Cais_Nome = Cais_nome

                    };

                    await AddArtigosEnviados(artigosPre);

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public async Task AddArtigosEnviados(ExpedicaoArtigo preparados)
        {
            HttpClient client = new HttpClient();

            Uri uri = new Uri("http://150.1.101.6:7000/api/encomendas/artigosEnv");
            HttpResponseMessage response = await client.PostAsync(uri, new StringContent(JsonSerializer.Serialize(preparados), Encoding.UTF8, "application/json"));

            response.EnsureSuccessStatusCode();

        }

        private async void cancelarbtn_Clicked(object sender, EventArgs e)
        {
            await PopupNavigation.Instance.PopAsync();
        }
    }
}