using LiteDB;
using M2UApp.ViewModel;
using Rg.Plugins.Popup.Extensions;
using System;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace M2UApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : ContentPage
    {


        public LoginPage()
        {

            InitializeComponent();
             
         //    InitializeAsync();
           

            /*  if(Settings.Login.Length > 0)
              {
                  Email.Text = Settings.Login;
              }*/
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

              public async Task RefreshDataAsync(string user, string pass)
              {

                  HttpClient client = new HttpClient();
                  Uri uri = new Uri("http://150.1.101.6:7000/api/login/login?user=" + user + "&password=" + GetHashString(pass));
                  HttpResponseMessage response = await client.GetAsync(uri);

                  if (response.IsSuccessStatusCode)
                  {

                      string content = await response.Content.ReadAsStringAsync();

                      var Valido = JsonSerializer.Deserialize(content);

                      if (Valido)
                      {
                          await Shell.Current.GoToAsync($"//{nameof(ArmazensPage)}");
                      }  else
                      {
                          await App.Current.MainPage.DisplayAlert("Login Falhou", "Email / Password estão incorretos", "OK");
                      }
                  }
              }

            private async void loginbtn_Clicked(object sender, EventArgs e)
            {

                if (string.IsNullOrEmpty(Username.Text) || string.IsNullOrEmpty(Password.Text))
                {
                    await App.Current.MainPage.DisplayAlert("Campos Vazios", "Introduza um Email e Password", "OK");


                }
                else
                {
                    await RefreshDataAsync(Username.Text,Password.Text);
                }

            

        }
            

            // Settings.Login = Email.Text;
            //  LiteCollection<UserLocal> lite = db.GetCollection<UserLocal>;
            /*    int iduser = UserLocals.Count() == 0 ? 1 : (int)(UserLocals.Max(x => x.id) + 1);

                UserLocal userLocal = new UserLocal
                {
                    id = iduser,
                    Email = Email.Text,
                };*/
            //       UserLocals.Insert(userLocal);

      //  }

        /*  private void Insert(object sender, EventArgs e)
          {
              int iduser = UserLocals.Count() == 0 ? 1 : (int)(UserLocals.Max(x => x.id) + 1);

              UserLocal userLocal = new UserLocal
              {
                  id = iduser,
                  Email = Email.Text,
              };

              UserLocals.Insert(userLocal);

          }*/
    }
}