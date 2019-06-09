using MasternodePools.Api.Attributes;
using MasternodePools.Api.Models;
using MasternodePools.Data.Entities;
using MasternodePools.Data.Services.Abstraction;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MasternodePools.Api.Controllers
{
    [Route("wallet")]
    [ApiController]
    public class WalletController : Controller
    {
        private IWalletService _walletService;

        public WalletController(IWalletService walletService)
        {
            _walletService = walletService;
        }

        [HttpGet("all")]
        [DiscordAuthorize]
        public async Task<IEnumerable<UserWallet>> GetAll()
        {
            var currentUser = (DiscordUser)HttpContext.Items["User"];
            var wallets = await _walletService.GetWalletsAsync(currentUser.Id);
            return wallets.Select(ToUserWallet);
        }

        [HttpGet("{coin}")]
        [DiscordAuthorize]
        public async Task<UserWallet> GetWallet(string coin)
        {
            var currentUser = (DiscordUser)HttpContext.Items["User"];
            var wallet = await _walletService.GetWalletAsync(currentUser.Id, coin);
            if (wallet == null)
            {
                wallet = await _walletService.CreateWalletAsync(currentUser.Id, coin);
            }
            return ToUserWallet(wallet);
        }

        private UserWallet ToUserWallet(Wallet wallet)
            => new UserWallet
            {
                Coin = wallet.Coin,
                Balance = wallet.Balance,
                Address = wallet.Address
            };
    }
}