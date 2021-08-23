﻿using M2UApp.Models;
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
    public partial class ListArtigosExecucao : ContentPage
    {
        public List<ExpedicaoArtigo> artigos;
        public string numero_encomenda;

        public ListArtigosExecucao()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            if (artigos == null)
            {
                PopupEncomendas popupEncomendas = new PopupEncomendas();
                popupEncomendas.EncomendaReaded += List;
                Navigation.PushPopupAsync(popupEncomendas);
            }

        }

        private void Subtrair_leitura_Clicked(object sender, EventArgs e)
        {

        }

        private async void Adicionar_Objeto_Clicked(object sender, EventArgs e)
        {
            ZXingView zXingView = new ZXingView("Leia o código de barras do artigo a ADICIONAR", "O Código será lido automaticamente");
            zXingView.BarcodeReaded += Adicionar_BarcodeReaded;
            await Navigation.PushModalAsync(zXingView);
        }

        private async void Select_Clicked(object sender, EventArgs e)
        {
            ZXingView zXingView = new ZXingView("Leia o código de barras do artigo a REMOVER", "O Código será lido automaticamente");
            zXingView.BarcodeReaded += Remover_BarcodeReaded;
            await Navigation.PushModalAsync(zXingView);
        }
        private async void List(object sender, string e)
        {
            numero_encomenda = e;
            ListArtigos.ItemsSource = await RefreshDataAsync(e);
        }

        public async Task<List<ExpedicaoArtigo>> RefreshDataAsync(string numero_encomenda)
        {
            artigos = new List<ExpedicaoArtigo>();
            HttpClient client = new HttpClient();
            Uri uri = new Uri("http://150.1.101.6:7000/api/encomendas/artigosExec?numero_encomenda=" + numero_encomenda);
            HttpResponseMessage responseMessage = await client.GetAsync(uri);

            if (responseMessage.IsSuccessStatusCode)
            {
                string context = await responseMessage.Content.ReadAsStringAsync();
                var artigo = JsonSerializer.Deserialize<List<ExpedicaoArtigo>>(context);

                artigos = new List<ExpedicaoArtigo>(artigo);
            }
            return artigos;
        }

        public async void ReceberCodigo(object sender, string e)
        {
            if (artigos.Where(f => f.Referencia_Artigo.Contains(e)).Count() == 1)
            {
                ExpedicaoArtigo aa = artigos.FirstOrDefault(f => f.Id == artigos.Where(x => x.Referencia_Artigo.Contains(e)).Select(x => x.Id).FirstOrDefault());
                //      Artigo aae = artigos.FirstOrDefault(f => f.Id == artigos.Where(x => x.Quantidade != x.QuantidadePicado).Select(x => x.Id).FirstOrDefault());
                ExpedicaoArtigo xx = artigos.Where(x => x.Referencia_Artigo.Contains(e)).FirstOrDefault();

                if (xx != null)
                {
                    xx.QuantidadePicado++;
                }

                if (aa.Quantidade < aa.QuantidadePicado)
                {
                    await Application.Current.MainPage.DisplayAlert("Aviso", "Quantidade superior", "OK");
                }

                ListArtigos.ItemsSource = new List<ExpedicaoArtigo>(artigos);

                await Application.Current.MainPage.DisplayAlert("Sucesso", "Artigo " + e + " adicionado", "OK");

            }
            else if (artigos.Where(f => f.Referencia_Artigo.Contains(e)).Count() == 0)
            {
                artigos.Add(new ExpedicaoArtigo()
                {//
                    Id = artigos.Count() + 1,
                    Referencia_Artigo = e,
                    Quantidade = 0,
                    QuantidadePicado = 1
                });

                ListArtigos.ItemsSource = new List<ExpedicaoArtigo>(artigos);

                await Application.Current.MainPage.DisplayAlert("Sucesso", "Artigo " + e + " adicionado", "OK");
            }
            else
            {
                await Application.Current.MainPage.DisplayAlert("Erro", "Erro Qr Code", "OK");
            }
        }

        public async void RemoverCodigo(object sender, string e)
        {
            if (artigos.Where(x => x.Referencia_Artigo.Contains(e)).Count() == 1)
            {

                ExpedicaoArtigo xx = artigos.Where(x => x.Referencia_Artigo.Contains(e)).FirstOrDefault();

                if ((xx != null && xx.QuantidadePicado > 1) || (xx.QuantidadePicado == 1 && xx.Quantidade > 1))
                {
                    xx.QuantidadePicado--;

                    ListArtigos.ItemsSource = new List<ExpedicaoArtigo>(artigos);

                    await Application.Current.MainPage.DisplayAlert("Sucesso", "Quantidade " + e + " removida", "OK");
                }
                else if (xx.QuantidadePicado == 1 && xx.Quantidade == 0)
                {
                    artigos.Remove(xx);

                    ListArtigos.ItemsSource = new List<ExpedicaoArtigo>(artigos);

                    await Application.Current.MainPage.DisplayAlert("Sucesso", "Artigo " + e + " removido", "OK");
                }
            }
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