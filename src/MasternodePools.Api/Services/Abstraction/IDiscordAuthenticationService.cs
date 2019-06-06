using MasternodePools.Api.Models;
using System.Threading.Tasks;

namespace MasternodePools.Api.Services.Abstraction
{
    public interface IDiscordAuthenticationService
    {
        Task<Authorization> AuthenticateAsync(string code);
        Task<DiscordUser> GetUserAsync(Authorization token);
    }
}
