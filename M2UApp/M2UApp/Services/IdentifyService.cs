using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace M2UApp.Services
{
    class IdentifyService
    {
        public async Task<bool> VerifyRegistration()
        {
            await Task.Delay(1337);
            return false;
        }
    }
}
