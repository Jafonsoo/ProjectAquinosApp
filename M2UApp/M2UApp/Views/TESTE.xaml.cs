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
    [QueryProperty("Entry", "entry")]
    public partial class TESTE : ContentPage
    {
        public string Entry
        { 
            set
            {
                textLabel2.Text = Uri.UnescapeDataString(value);
            }

        }

        public TESTE()
        {
            InitializeComponent();
        }

    }
}