using M2UApp.Views;
using M2ULogistic.ViewModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
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
        private string email;
        public string Email
        {
            get { return email; }
            set
            {
                email = value;
                PropertyChanged(this, new PropertyChangedEventArgs("Email"));
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
        public Command LoginCommand
        {
            get
            {
               return new Command(Login);
            }
        }
        public Command SignUp
        {
            get
            {
                return new Command(()=> { App.Current.MainPage.Navigation.PushAsync(new SignUpPage()); });
            }
        }

       private async void Login()
        {
            var date = DateTime.Now;

            if (string.IsNullOrEmpty(Email) || string.IsNullOrEmpty(Password))
             await App.Current.MainPage.DisplayAlert("Campos Vazios", "Introduza um Email e Password", "OK");
            else
            {

                var user = await FirebaseHelper.GetUser(Email);


                if(user!=null)
                if (Email == user.Email && Password == user.Password)
                {
                await  App.Current.MainPage.DisplayAlert("Login Conseguido", date.ToString(), "Ok");

                        await Shell.Current.GoToAsync($"//AboutPage?entry={email}");

                    }
                else
                await  App.Current.MainPage.DisplayAlert("Login Falhou", "Email e Password estão incorretos", "OK");
                else
                    await App.Current.MainPage.DisplayAlert("Login Falhou", "Utilizador não encontrado", "OK");
            }
        }
    }
}
