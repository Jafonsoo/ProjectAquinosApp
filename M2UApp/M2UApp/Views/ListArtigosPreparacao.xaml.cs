using M2UApp.Models;
using Newtonsoft.Json;
using Rg.Plugins.Popup.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace M2UApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ListArtigosPreparacao : ContentPage
    {
        public List<Artigo> artigos;
        public int numero_encomenda;
        public Artigo artigo;
        public string nome_artigo;
        public int i;

        public ListArtigosPreparacao()
        {
            InitializeComponent();
        }

        public List<Artigo> GetArtigos()
        { 
            return artigos; 
        }

        protected override void OnAppearing()
        {

            if(artigos == null) { 
            PopupEncomendas popupEncomendas = new PopupEncomendas();
            popupEncomendas.EncomendaReaded += List;
            Navigation.PushPopupAsync(popupEncomendas);
            }

        }
        private async void Adicionar_Objeto_Clicked(object sender, EventArgs e)
        {
            ZXingView zXingView = new ZXingView("Leia o código de barras do artigo a ADICIONAR", "O Código será lido automaticamente");
            zXingView.BarcodeReaded += ZXingView_BarcodeReaded;
            await Navigation.PushModalAsync(zXingView);
        }

   /*    private async void Button_Clicked_1(object sender, EventArgs e)
        {
            ZXingView zXingView = new ZXingView("Leia o código de barras do artigo a ADICIONAR", "O Código será lido automaticamente");
            zXingView.BarcodeReaded += ZXingView_BarcodeReaded;


            await Navigation.PushModalAsync(zXingView);
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            PopupEncomendas popupEncomendas = new PopupEncomendas();
            popupEncomendas.EncomendaReaded += List;
            await Navigation.PushPopupAsync(popupEncomendas);
        }*/
        private async void List(object sender, int e)
        {
            numero_encomenda = e;
            ListArtigos.ItemsSource = await RefreshDataAsync(e);
        }

        public async Task<List<Artigo>> RefreshDataAsync(int numero_encomenda)
        {
            artigos = new List<Artigo>();
            HttpClient client = new HttpClient();
            Uri uri = new Uri("http://150.1.101.6:7000/api/encomendas/artigos?numero_encomenda=" + numero_encomenda);
            HttpResponseMessage responseMessage = await client.GetAsync(uri);

            if (responseMessage.IsSuccessStatusCode)
            {
                string context = await responseMessage.Content.ReadAsStringAsync();
                var artigo = JsonConvert.DeserializeObject<List<Artigo>>(context);
                 
                artigos = new List<Artigo>(artigo);
            }
            return artigos;
        }

        public async void ReceberCodigo(object sender, string e)
        {
            nome_artigo = e;
            numero_encomenda = i;
            if (artigos.Where(f => f.Referencia_Artigo.Contains(e)).Count() == 1 &&
                artigos.Where(f => f.Referencia_Artigo.Contains(e)).Select(x => x.Quantidade > 0).FirstOrDefault())
            {
                Artigo aa = artigos.FirstOrDefault(f => f.Id == artigos.Where(x => x.Referencia_Artigo.Contains(e)).Select(x => x.Id).FirstOrDefault());
          //      Artigo aae = artigos.FirstOrDefault(f => f.Id == artigos.Where(x => x.Quantidade != x.QuantidadePicado).Select(x => x.Id).FirstOrDefault());

                if (aa != null) 
                {
                    aa.QuantidadePicado++;
                }

                if(aa.Quantidade < aa.QuantidadePicado)
                {
                    await Application.Current.MainPage.DisplayAlert("Aviso", "Quantidade superior", "OK");
                } 

                ListArtigos.ItemsSource = new List<Artigo>(artigos);

                await Application.Current.MainPage.DisplayAlert("Sucesso", "Artigo "  + nome_artigo + " adicionado" , "OK");//Duvida tirar ou nao
                
                if(artigos.Sum(x => x.QuantidadePicado) == 4)
                {
                    foreach(Artigo artigo in artigos)
                    {
                    await Application.Current.MainPage.DisplayAlert("Erro", "ENVIAR "+ artigo.Quantidade , "OK");

                    }
                }
            }else
            {
            await Application.Current.MainPage.DisplayAlert("Erro", "Artigo não encontrado", "OK");
            }
        }

        public void NumeroSerie(object sender, string e)
        {
            artigos = new List<Artigo>();

            artigos.Add(new Artigo()
            {
                NumeroSerie = e
            });
            
            
        } 

        void ZXingView_BarcodeReaded(object sender, string e)
        {
            string[] delimiterChar = { "#", "" };

            var splitArray = e.Split(delimiterChar, StringSplitOptions.RemoveEmptyEntries).ToList();
            splitArray.RemoveAt(0);
            string combindedString = string.Join("", splitArray);

            ReceberCodigo(sender, combindedString);

          /*  var splitSerie = e.Split(delimiterChar, StringSplitOptions.RemoveEmptyEntries).ToList();
            splitSerie.RemoveAt(1);
            string combindedString2 = string.Join("", splitSerie);

            NumeroSerie(sender, combindedString2);*/

        }

        private void Subtrair_leitura_Clicked(object sender, EventArgs e)
        {

        }
    }
}