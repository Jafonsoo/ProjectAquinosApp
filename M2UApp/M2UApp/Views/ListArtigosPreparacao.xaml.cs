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
        public List<ArtigosTest> artigosPicados;
        public string numero_encomenda;
        public string nome_artigo;
        public string numero_serie;

        public ListArtigosPreparacao()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            if (artigos == null) 
            { 
            PopupEncomendas popupEncomendas = new PopupEncomendas();
            popupEncomendas.CloseWhenBackgroundIsClicked = false;
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
        
        private void Select_Clicked(object sender, EventArgs e)
        {
            EnviarArtigos(sender, artigosPicados);
        }

        private async void List(object sender, string e)
        {
            if(e != null) {
            numero_encomenda = e;

            var listArtigos = await RefreshDataAsync(e);

            if(listArtigos.Count() != 0)
                {
                   ListArtigos.ItemsSource = await RefreshDataAsync(e);
                }
            else
                {
                await Application.Current.MainPage.DisplayAlert("Erro", "Numero da encomenda não processado", "OK");
                await Shell.Current.GoToAsync("..");
                }
            }
            else if(artigos == null)
            {
                await Application.Current.MainPage.DisplayAlert("Erro", "Nenhuma encomenda introduzida", "OK");
                await Shell.Current.GoToAsync("..");
            }
        }

        public async Task<List<Artigo>> RefreshDataAsync(string numero_encomenda)
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
         
        public async Task<List<Artigo>> RefreshDataAsyncOriginal(string numero_encomenda)
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


            public async void ReceberCodigo(object sender, string e, string b)
            {
            nome_artigo = e;
            numero_serie = b;
            if (artigos.Where(f => f.Referencia_Artigo.Contains(e)).Count() == 1)
            {
                Artigo artigos_Referencia = artigos.Where(x => x.Referencia_Artigo.Contains(e)).FirstOrDefault();

                    if (artigosPicados == null)
                    {
                        artigosPicados = new List<ArtigosTest>();

                        artigosPicados.Add(new ArtigosTest()
                        {
                            Id = 0,
                            Referencia_Artigo = nome_artigo,
                            NumeroSerie = numero_serie
                        });

                    if (artigos_Referencia != null)
                    {
                        artigos_Referencia.QuantidadePicado++;
                    }

                    ListArtigos.ItemsSource = new List<Artigo>(artigos);

                    await Application.Current.MainPage.DisplayAlert("Sucesso", "Artigo " + nome_artigo + " adicionado", "OK");

                    }
                    else if (artigosPicados.Where(x => x.NumeroSerie.Contains(b)).Count() == 0)
                    {
                        artigosPicados.Add(new ArtigosTest()
                        {
                            Id = artigosPicados.Count(),
                            Referencia_Artigo = nome_artigo,
                            NumeroSerie = numero_serie
                        });

                    if (artigos_Referencia != null)
                    {
                        artigos_Referencia.QuantidadePicado++;
                    }


                    ListArtigos.ItemsSource = new List<Artigo>(artigos);

                    await Application.Current.MainPage.DisplayAlert("Sucesso", "Artigo " + nome_artigo + " adicionado", "OK");
                    }
                    else
                    {
                    await Application.Current.MainPage.DisplayAlert("Erro", "Artigo já registado!", "OK");
                    }

            } 
            else if (artigos.Where(f => f.Referencia_Artigo.Contains(e)).Count() == 0)
            {

                    if (artigosPicados == null)
                    {
                        artigosPicados.Add(new ArtigosTest()
                        {
                            Id = 0,
                            Referencia_Artigo = nome_artigo,
                            NumeroSerie = numero_serie
                        });

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
                    else if (artigosPicados.Where(x => x.NumeroSerie.Contains(b)).Count() == 0)
                    {
                        artigosPicados.Add(new ArtigosTest()
                        {
                            Id = artigosPicados.Count(),
                            Referencia_Artigo = nome_artigo,
                            NumeroSerie = numero_serie
                        });

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
                    await Application.Current.MainPage.DisplayAlert("Erro", "Artigo já registado!", "OK");
                }
            }
            else
            {
            await Application.Current.MainPage.DisplayAlert("Erro", "Erro Qr Code", "OK");
            }
        }

        public async void EnviarArtigos(object sender, List<ArtigosTest> artigosPicado)
        {
            artigosOriginal = await RefreshDataAsyncOriginal(numero_encomenda);
            artigosPicado = artigosPicados;
            IEnumerable<Artigo> comparaList = artigos.Where(x => !artigosOriginal.Any(z => x.Id == z.Id 
            && x.Referencia_Artigo == z.Referencia_Artigo 
            && x.QuantidadePicado == z.Quantidade));
             
            PopupArtigos popup = new PopupArtigos();

            popup.ListElementos(comparaList,artigosPicado,numero_encomenda);

            await Navigation.PushPopupAsync(popup);
        } 

        public async void RemoverCodigo(object sender, string e, string b)
        {
            if(artigosPicados == null)
            {
                await Application.Current.MainPage.DisplayAlert("Erro", "Nenhum artigo adicionado", "OK");
            }
            else if(artigos.Where(x => x.Referencia_Artigo.Contains(e)).Count() == 1)
            {
                Artigo artigos_referencia = artigos.Where(x => x.Referencia_Artigo.Contains(e)).FirstOrDefault();
                int artigosTest = artigosPicados.Where(x=> x.NumeroSerie.Contains(b)).Count();
                ArtigosTest test = artigosPicados.Where(x => x.NumeroSerie.Contains(b)).FirstOrDefault();

                if (((artigos_referencia != null && artigos_referencia.QuantidadePicado > 1) || (artigos_referencia.QuantidadePicado == 1 && artigos_referencia.Quantidade > 1)) && artigosTest == 1 )
                {
                    artigos_referencia.QuantidadePicado--;

                  if(artigosPicados.Count() > 1)
                    {
                        artigosPicados.Remove(test);
                    }
                    else
                    {
                        artigosPicados.Clear();
                        artigosPicados = new List<ArtigosTest>(artigosPicados);
                    }

                    ListArtigos.ItemsSource = new List<Artigo>(artigos);

                    await Application.Current.MainPage.DisplayAlert("Sucesso", "Quantidade " + e + " removida", "OK");

  

                }
                else if (artigos_referencia.QuantidadePicado == 1 && artigos_referencia.Quantidade == 0 && artigosTest == 1)
                {
                    artigos.Remove(artigos_referencia);

                    if (artigosPicados.Count() > 1)
                    {
                        artigosPicados.Remove(test);
                    }
                    else
                    {
                        artigosPicados.Clear();
                        artigosPicados = new List<ArtigosTest>(artigosPicados);
                    }

                    ListArtigos.ItemsSource = new List<Artigo>(artigos);

                    await Application.Current.MainPage.DisplayAlert("Sucesso", "Artigo " + e + " removido", "OK");
                }
            }
            else
            {
                await Application.Current.MainPage.DisplayAlert("", "Impossível remover artigo", "OK");
            }
        }


        void Adicionar_BarcodeReaded(object sender, string e)
        {
            string[] delimiterChar = { "#", "" };

            var splitArray = e.Split(delimiterChar, StringSplitOptions.RemoveEmptyEntries).ToList();
            splitArray.RemoveAt(0);
            string combindedString = string.Join("", splitArray);
            var splitSerie = e.Split(delimiterChar, StringSplitOptions.RemoveEmptyEntries).ToList();
            splitSerie.RemoveAt(1);
            string combindedString2 = string.Join("", splitSerie);

            ReceberCodigo(sender, combindedString, combindedString2);
        }

        void Remover_BarcodeReaded(object sender, string e) 
        {
            string[] delimiterChar = { "#", "" };

            var splitArray = e.Split(delimiterChar, StringSplitOptions.RemoveEmptyEntries).ToList();
            splitArray.RemoveAt(0);
            string combindedString = string.Join("", splitArray);
            var splitSerie = e.Split(delimiterChar, StringSplitOptions.RemoveEmptyEntries).ToList();
            splitSerie.RemoveAt(1);
            string combindedString2 = string.Join("", splitSerie);

            RemoverCodigo(sender, combindedString,combindedString2);
        }
    }
}