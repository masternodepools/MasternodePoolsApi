using MasternodePools.Data.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MasternodePools.Data.Services.Abstraction
{
    public interface IWalletService
    {
        Task<IList<Wallet>> GetWalletsAsync(string userId);

        Task<Wallet> GetWalletAsync(string userId, string coin);

        Task<Wallet> CreateWalletAsync(string userId, string coin);
    }
}
