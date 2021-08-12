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
    public partial class AdicionarPreCarga : ContentPage
    {
        public AdicionarPreCarga()
        {
            InitializeComponent();
            NomeArmazem.Text = (string)Application.Current.Properties["ArmazemAtual"];
        }
    }
}