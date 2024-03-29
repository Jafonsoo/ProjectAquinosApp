﻿
using M2UApp.Views;
using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace M2UApp
{
    public partial class AppShell : Shell
    {

        public AppShell()
        {
            InitializeComponent();

            Routing.RegisterRoute(nameof(LoginPage), typeof(LoginPage));
            Routing.RegisterRoute(nameof(Definicoes), typeof(Definicoes));
            Routing.RegisterRoute(nameof(ExpedicaoPage), typeof(ExpedicaoPage));
            Routing.RegisterRoute(nameof(AboutPage), typeof(AboutPage));
            Routing.RegisterRoute(nameof(ArmazensPage), typeof(ArmazensPage));
            Routing.RegisterRoute(nameof(InternosPage), typeof(InternosPage));
            Routing.RegisterRoute(nameof(Estatisticas), typeof(Estatisticas));
            Routing.RegisterRoute(nameof(PreparacaoPage), typeof(PreparacaoPage));
            Routing.RegisterRoute(nameof(ExecucaoPage), typeof(ExecucaoPage));
            Routing.RegisterRoute(nameof(TabbedPrep), typeof(TabbedPrep));
            Routing.RegisterRoute(nameof(TabbedExec), typeof(TabbedExec));
            Routing.RegisterRoute(nameof(AdicionarExecCarga), typeof(AdicionarExecCarga));
            Routing.RegisterRoute(nameof(ListArtigosPreparacao), typeof(ListArtigosPreparacao));
            Routing.RegisterRoute(nameof(ListArtigosExecucao), typeof(ListArtigosExecucao));

            
        }

        private async void OnMenuItemClicked(object sender, EventArgs e)
        {
            bool action = await DisplayAlert("", "Pretende Terminar a sessão?", "Sim", "Não");

            if (action)
            {
                Application.Current.Properties["ArmazemAtual"] = null;
                await Shell.Current.GoToAsync($"//LoginPage");
            }
            else { }
        }

        private async void MenuItem_Clicked(object sender, EventArgs e)
        {
            bool action = await DisplayAlert("", "Pretende alterar o armazem?", "Sim", "Não");

            if (action)
            {
                Application.Current.Properties.Remove("ArmazemAtual");
                await Shell.Current.GoToAsync($"//ArmazensPage");
            }
            else { }
        }
    }
}
