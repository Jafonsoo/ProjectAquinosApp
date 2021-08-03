using M2UApp.ViewModels;
using System.ComponentModel;
using Xamarin.Forms;

namespace M2UApp.Views
{
    public partial class ItemDetailPage : ContentPage
    {
        public ItemDetailPage()
        {
            InitializeComponent();
            BindingContext = new ItemDetailViewModel();
        }
    }
}