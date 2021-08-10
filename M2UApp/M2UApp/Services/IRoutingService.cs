using System.Threading.Tasks;

namespace M2UApp.Services
{
    public interface IRoutingService
    {
        Task RefreshDataAsync(string user, string pass);
        Task NavigateTo(string v);
    }
}
