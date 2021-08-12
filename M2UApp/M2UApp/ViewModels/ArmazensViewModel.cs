using M2UApp.Models;
using M2UApp.Views;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace M2UApp.ViewModels
{
    public class ArmazensViewModel : BaseViewModel
    {
        public Armazens armazens2;
        public Command<Armazens> ItemTappe { get; } 

        public ArmazensViewModel() 
        {
            Title = "duc"; 

            ItemTappe = new Command<Armazens>(onArmazemSelect);
        }

        public void onAppearing()
        {
            SelectArmazem = null;
        }


        public Armazens SelectArmazem
        {
            get => armazens2;
            set
            {
                SetProperty(ref armazens2, value);

                onArmazemSelect(value);
                
            }
        }
         
         

        async void onArmazemSelect(Armazens armazens)
        {
            if (armazens == null)
                return;

            await Shell.Current.GoToAsync($"//{nameof(AboutPage)}");
        }
    }
}
