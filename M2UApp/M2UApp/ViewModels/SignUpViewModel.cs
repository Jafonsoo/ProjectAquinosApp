using M2UApp.Views;
using M2ULogistic.ViewModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Text.RegularExpressions;
using Xamarin.Forms;

namespace M2UApp.ViewModels
{
    class SignUpViewModel : INotifyPropertyChanged
    {
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

        public event PropertyChangedEventHandler PropertyChanged;

        public string Password
        {
            get { return password; }
            set
            {
                password = value;
                PropertyChanged(this, new PropertyChangedEventArgs("Password"));
            }
        }

        private string confirmpassword;
        public string ConfirmPassword
        {
            get { return confirmpassword; }
            set
            {
                confirmpassword = value;
                PropertyChanged(this, new PropertyChangedEventArgs("ConfirmPassword"));
            }
        }
        public Command SignUpCommand
        {
            get
            {
                return new Command(() =>
                {
                    if (Password == ConfirmPassword)
                        SignUp();
                    else
                        App.Current.MainPage.DisplayAlert("", "Password´s não coincidem", "OK");
                });
            }
        }

        private async void SignUp()
        {
            var emailPattern = "^([\\w\\.\\-]+)@([\\w\\-]+)((\\.(\\w){2,3})+)$";
            var date = DateTime.Now;

            if (string.IsNullOrEmpty(Email) || string.IsNullOrEmpty(Password))
                await Application.Current.MainPage.DisplayAlert("Campos Vazios", "Introduza um Email e Password", "OK");
            else if (!string.IsNullOrWhiteSpace(email) && !Regex.IsMatch(email, emailPattern))
            {
                await Application.Current.MainPage.DisplayAlert("Email Incorreto", "Por favor introduza um Email válido", "OK");
            }
            else  
            {
                //call AddUser function which we define in Firebase helper class
                var user = await FirebaseHelper.AddUser(Email, Password);
                //AddUser return true if data insert successfuly 
                if (user)
                {
                    await Application.Current.MainPage.DisplayAlert("Registo Concluido", date.ToString(), "Ok");
                    //Navigate to Wellcom page after successfuly SignUp
                    //pass user email to welcom page
                    await Shell.Current.GoToAsync($"//{nameof(LoginPage)}");
                } 
                else
                    await Application.Current.MainPage.DisplayAlert("Erro", "Registo Falhou", "OK");

            }
        }

    }
}
