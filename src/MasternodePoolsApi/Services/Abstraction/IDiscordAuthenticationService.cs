using MasternodePoolsApi.Models;
using System.Threading.Tasks;

namespace MasternodePoolsApi.Services.Abstraction
{
    public interface IDiscordAuthenticationService
    {
        Task<Authorization> AuthenticateAsync(string code);
        Task<DiscordUser> GetUser(Authorization token);
    }
}
