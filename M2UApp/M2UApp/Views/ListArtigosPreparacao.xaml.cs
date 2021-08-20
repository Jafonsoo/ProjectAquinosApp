using M2UApp.Models;
using Rg.Plugins.Popup.Extensions;
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
    public partial class ListArtigosPreparacao : ContentPage
    {
        public List<Artigo> artigos;
        public List<Artigo> artigosOriginal;
        public int numero_encomenda;
        public Artigo artigo;
        public string nome_artigo;

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
            zXingView.BarcodeReaded += Adicionar_BarcodeReaded;
            await Navigation.PushModalAsync(zXingView);
        }
        private async void Subtrair_leitura_Clicked(object sender, EventArgs e)
        {
            ZXingView zXingView = new ZXingView("Leia o código de barras do artigo a REMOVER", "O Código será lido automaticamente");
            zXingView.BarcodeReaded += Remover_BarcodeReaded;
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
        
        private void Select_Clicked(object sender, EventArgs e)
        {
            EnviarArtigos(sender, artigos);
        }

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
                var artigo = JsonSerializer.Deserialize<List<Artigo>>(context);

                artigos = new List<Artigo>(artigo);
            }
            return artigos;
        }
         
        public async Task<List<Artigo>> RefreshDataAsyncOriginal(int numero_encomenda)
        {
            artigosOriginal = new List<Artigo>();
            HttpClient client = new HttpClient();
            Uri uri = new Uri("http://150.1.101.6:7000/api/encomendas/artigos?numero_encomenda=" + numero_encomenda);
            HttpResponseMessage responseMessage = await client.GetAsync(uri);

            if (responseMessage.IsSuccessStatusCode)
            {
                string context = await responseMessage.Content.ReadAsStringAsync();
                var artigo = JsonSerializer.Deserialize<List<Artigo>>(context);

                artigosOriginal = new List<Artigo>(artigo);
            }
            return artigosOriginal;
        }


            public async void ReceberCodigo(object sender, string e)
        {
            nome_artigo = e;
            if (artigos.Where(f => f.Referencia_Artigo.Contains(e)).Count() == 1)
            {
                Artigo aa = artigos.FirstOrDefault(f => f.Id == artigos.Where(x => x.Referencia_Artigo.Contains(e)).Select(x => x.Id).FirstOrDefault());
                //      Artigo aae = artigos.FirstOrDefault(f => f.Id == artigos.Where(x => x.Quantidade != x.QuantidadePicado).Select(x => x.Id).FirstOrDefault());
                Artigo xx = artigos.Where(x => x.Referencia_Artigo.Contains(e)).FirstOrDefault();

                if (xx != null) 
                {
                    xx.QuantidadePicado++;
                }

                if(aa.Quantidade < aa.QuantidadePicado)
                {
                    await Application.Current.MainPage.DisplayAlert("Aviso", "Quantidade superior", "OK");
                } 

                ListArtigos.ItemsSource = new List<Artigo>(artigos);

                await Application.Current.MainPage.DisplayAlert("Sucesso", "Artigo "  + nome_artigo + " adicionado" , "OK");
                
                if(artigos.Sum(x => x.QuantidadePicado) == 4)
                {
                    foreach(Artigo artigo in artigos)
                    {
                    await Application.Current.MainPage.DisplayAlert("Erro", "ENVIAR "+ artigo.QuantidadePicado , "OK");

                    }
                }
            } else if (artigos.Where(f => f.Referencia_Artigo.Contains(e)).Count() == 0)
            {
                artigos.Add(new Artigo()
                {
                    Id = artigos.Count() + 1,
                    Referencia_Artigo = e,
                    Quantidade = 0,
                    QuantidadePicado = 1
                });

                ListArtigos.ItemsSource = new List<Artigo>(artigos);

                await Application.Current.MainPage.DisplayAlert("Sucesso", "Artigo " + nome_artigo + " adicionado", "OK");
            }
            else
            {
            await Application.Current.MainPage.DisplayAlert("Erro", "Erro Qr Code", "OK");
            }
        }

        public async void EnviarArtigos(object sender, List<Artigo> artigos)
        {
                 artigosOriginal = await RefreshDataAsyncOriginal(numero_encomenda);
            //  List<Artigo> list3 = artigosOriginal.Where(x => x.Referencia_Artigo.Contains(artigos)).ToList();
            //   var list3 = artigos.Where(x => artigosOriginal.Any(z => x.Id == z.Id && x.Referencia_Artigo == z.Referencia_Artigo)
            // && !artigosOriginal.Any(z => x.Id == z.Id && x.Referencia_Artigo == z.Referencia_Artigo));
            IEnumerable<Artigo> list3 = artigos.Where(x => !artigosOriginal.Any(z => x.Id == z.Id 
            && x.Referencia_Artigo == z.Referencia_Artigo 
            && x.QuantidadePicado == z.Quantidade));

            string aa= string.Concat(list3.Select(x=> x.Referencia_Artigo + "#" + x.QuantidadePicado + "#"));


        //    foreach (Artigo artigo in list3)
      //     && !artigosOriginal.Any(z => x.Id == z.Id && x.Referencia_Artigo == z.Referencia_Artigo)))
        //    {
                //await Application.Current.MainPage.DisplayAlert("ole", "jasj" + artigo.Referencia_Artigo, "ok");
                PopupArtigos popup = new PopupArtigos();
                popup.ListElementos(list3,aa,numero_encomenda);
                await Navigation.PushPopupAsync(popup);


         //   await Application.Current.MainPage.DisplayAlert("ole", "jasj" + aa, "ok");

            //   }

        } 

        public async void RemoverCodigo(object sender, string e)
        {
            if(artigos.Where(x => x.Referencia_Artigo.Contains(e)).Count() == 1)
            {

                Artigo xx = artigos.Where(x => x.Referencia_Artigo.Contains(e)).FirstOrDefault();

                if ((xx != null && xx.QuantidadePicado > 1) || (xx.QuantidadePicado == 1 && xx.Quantidade > 1))
                {
                    xx.QuantidadePicado--;

                    ListArtigos.ItemsSource = new List<Artigo>(artigos);

                    await Application.Current.MainPage.DisplayAlert("Sucesso", "Quantidade " + e + " removida", "OK");
                }
                else if (xx.QuantidadePicado == 1 && xx.Quantidade == 0)
                {
                    artigos.Remove(xx);

                    ListArtigos.ItemsSource = new List<Artigo>(artigos);

                    await Application.Current.MainPage.DisplayAlert("Sucesso", "Artigo " + e + " removido", "OK");
                }
            }
        }

        public void NumeroSerie(object sender, string e)
        {
            artigos.Add(new Artigo()
            {
                NumeroSerie = e
            });
        } 

        void Adicionar_BarcodeReaded(object sender, string e)
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

        void Remover_BarcodeReaded(object sender, string e) 
        {
            string[] delimiterChar = { "#", "" };

            var splitArray = e.Split(delimiterChar, StringSplitOptions.RemoveEmptyEntries).ToList();
            splitArray.RemoveAt(0);
            string combindedString = string.Join("", splitArray);

            RemoverCodigo(sender, combindedString);
        }
    }
}