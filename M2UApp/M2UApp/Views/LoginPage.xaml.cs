using M2UApp.ViewModel;
using M2UApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace M2UApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : ContentPage
    {

        LoginVM loginViewModel;
        public LoginPage()
        {
            loginViewModel = new LoginVM();
            InitializeComponent();
            BindingContext = loginViewModel;
        }
    }
}