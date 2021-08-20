using M2UApp.Models;
using M2UApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace M2UApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ArmazensPage : ContentPage
    {
        public List<Armazens> armazens;
        public Armazens armazens2; 

        public ArmazensPage()
        {
            InitializeComponent();
        } 

        protected async override void OnAppearing()
        {
            base.OnAppearing();
            
            ListArmazens.ItemsSource = await RefreshDataAsync();
            
        }

        public async Task<List<Armazens>> RefreshDataAsync()
        {
            armazens = new List<Armazens>();
            HttpClient client = new HttpClient();
            Uri uri = new Uri("http://150.1.101.6:7000/api/armazem");
            HttpResponseMessage response = await client.GetAsync(uri);

            if (response.IsSuccessStatusCode)
            {
                string content = await response.Content.ReadAsStringAsync();
                var armazem = JsonSerializer.Deserialize<List<Armazens>>(content);

                armazens = new List<Armazens>(armazem);
            }
                return armazens;
        }

        private async void Select_Clicked(object sender, EventArgs e)
        {
            if(armazens2 != null) {

                //  App.Current.MainPage.DisplayAlert("Valido", "selecionado", "OK");
                bool action = await DisplayAlert("", "Deseja selecionar o armazem " + armazens2.NomeArmazem, "Sim", "Não");

                if (action)
                {

                Application.Current.Properties["IdArmazem"] = armazens2.Id;
                Application.Current.Properties["ArmazemAtual"] = armazens2.NomeArmazem;

                await Shell.Current.GoToAsync($"//{nameof(AboutPage)}");

                }
                else 
                {}
            }
            else
            {
            await App.Current.MainPage.DisplayAlert("Erro", "Nenhum armazem selecionado", "OK");
            }
        }

     /*   void ShowDoneToolbarItem(bool show, ToolbarItem item = null)
        {
            if (show)
            {
                ToolbarItem done = new ToolbarItem();
                done.IconImageSource = "selecionado.png";
                done.Clicked += Select_Clicked;
                ToolbarItems.Add(done);
            }
            else if (item != null)
            {
                ToolbarItems.Remove(item);
            }
        }*/


        private void ListArmazens_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var armazensSelecionado = e.SelectedItem as Armazens;

            if (armazensSelecionado != null )
            {
                armazens2 = armazensSelecionado;

            }
        }

         void Search_TextChanged(object sender, TextChangedEventArgs e)
        {
            var keyword = Search.Text;
            /*     var sugestao = armazens.Where(c => c.nomeArmazem.ToLower().Contains(keyword.ToLower()));
                 ListArmazens.ItemsSource = sugestao;*/
            var newTextValue = Search.Text?.ToLower() ?? "";


            ListArmazens.ItemsSource = armazens.Where(f => f.NomeArmazem.ToLowerInvariant().Contains(newTextValue)).ToList();
        }
    }
}
