using M2UApp.Services;
using Splat;
using System;
using System.Collections.Generic;
using System.Text;

namespace M2UApp.ViewModels
{
    class LoadingViewModel : BaseViewModel
    {
        private readonly IRoutingService routingServce;
        private readonly IdentifyService identifyService;

        public LoadingViewModel(IRoutingService routingService = null, IdentifyService identityService = null)
        {
            this.routingServce = routingService ?? Locator.Current.GetService<IRoutingService>();
            this.identifyService = identityService ?? Locator.Current.GetService<IdentifyService>();
        }

        public async void Init()
        {
            var isAuthenticated = await this.identifyService.VerifyRegistration();
            if (isAuthenticated)
            {
                await this.routingServce.NavigateTo("///main");
            }
            else
            {
                await this.routingServce.NavigateTo("///login");
            }
        }
    }
}
