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
    public partial class TestQR : ContentPage
    {


        public TestQR()
        {
            InitializeComponent();

            btnQrcode.Clicked += async (sender, e) =>
            {

                ZXingView zXingView = new ZXingView();
                zXingView.BarcodeReaded += ZXingView_BarcodeReaded;

                await Navigation.PushModalAsync(zXingView);
            };


        }
            void ZXingView_BarcodeReaded(object sender, string e)
            {
                lblResultado.Text = "QRCODE: " + e;
            }

    }
}