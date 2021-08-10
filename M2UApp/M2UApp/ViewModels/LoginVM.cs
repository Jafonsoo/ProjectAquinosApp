using LiteDB;
using M2UApp.Helpers;
using M2UApp.Models;
using M2UApp.Services;
using M2UApp.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace M2UApp.ViewModel
{
    public class LoginVM : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public LoginVM()
        {

        }
        private string username;
        public string Username
        {
            get { return username; }
            set 
            {
                username = value;
                PropertyChanged(this, new PropertyChangedEventArgs("Username"));
                
            }
        }
        private string password;
        public string Password
        {
            get { return password; }
            set
            {
                password = value;
                PropertyChanged(this, new PropertyChangedEventArgs("Password"));
            }
        }

        public static byte[] GetHash(string inputString)
        {
            using (HashAlgorithm algorithm = SHA256.Create())
                return algorithm.ComputeHash(Encoding.UTF8.GetBytes(inputString));
        }


        public static string GetHashString(string inputString)
        {
            StringBuilder sb = new StringBuilder();
            foreach (byte b in GetHash(inputString))
                sb.Append(b.ToString("X2"));

            return sb.ToString();
        }

        public async Task loginWebServer(string user, string pass)
        {
            HttpClient client = new HttpClient();
            Uri uri = new Uri("http://150.1.101.6:7000/api/login/login?user=" + user + "&password=" + GetHashString(pass));
            HttpResponseMessage response = await client.GetAsync(uri);


            if (string.IsNullOrEmpty(user) || string.IsNullOrEmpty(pass))
            {
                await App.Current.MainPage.DisplayAlert("Campos Vazios", "Introduza um Email e Password", "OK");
                if (response.IsSuccessStatusCode)
                {

                    string content = await response.Content.ReadAsStringAsync();

                    var Valido = JsonSerializer.Deserialize(content);
                
                if (!Valido)
                {
                    await App.Current.MainPage.DisplayAlert("Login Falhou", "Username / Password estão incorretos", "OK");
                }
                else
                {

                    await Shell.Current.GoToAsync($"//AboutPage");
                 }
                }
            }

            }
        

                public Command LoginCommand
                { 

                    get
                    {

                    return new Command(async () => await loginWebServer(username, password));
                

                    }
                }



        /*   private async void Login()
           {
               var date = DateTime.Now;

               if (string.IsNullOrEmpty(Email) || string.IsNullOrEmpty(Password))
                await App.Current.MainPage.DisplayAlert("Campos Vazios", "Introduza um Email e Password", "OK");
               else
               {

               //    var user = await FirebaseHelper.GetUser(Email);


                   if(user!=null)
                   if (Email == user.Email && Password == user.Password)
                   {

                           await Shell.Current.GoToAsync($"//AboutPage");

                       }
                   else
                   await  App.Current.MainPage.DisplayAlert("Login Falhou", "Email e Password estão incorretos", "OK");
                   else
                       await App.Current.MainPage.DisplayAlert("Login Falhou", "Utilizador não encontrado", "OK");
               }
           }*/
    }
}

