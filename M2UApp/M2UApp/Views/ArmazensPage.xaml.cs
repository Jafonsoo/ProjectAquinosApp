using M2UApp.Models;
using Newtonsoft.Json;
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
    public partial class ArmazensPage : ContentPage
    {
        public ArmazensPage()
        {
            InitializeComponent();
        }
        public List<Armazens> armazens;

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
                var armazem = JsonConvert.DeserializeObject<List<Armazens>>(content);

                armazens = new List<Armazens>(armazem);
            }

                return armazens;
            }
        }
    }
