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
    public partial class Definicoes : ContentPage
    {
        DefinicoesViewModel definicoes;


        public Definicoes()
        {
            InitializeComponent();
            definicoes = new DefinicoesViewModel("vfref");
            BindingContext = definicoes;
        }
    }
}