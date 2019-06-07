using MasternodePools.Data.Context;
using MasternodePools.Data.Entities;
using MasternodePools.Data.Services.Abstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MasternodePools.Data.Services
{
    public class WalletService : IWalletService
    {
        private DynamoContext _dynamoContext;

        public WalletService(DynamoContext dynamoContext)
        {
            _dynamoContext = dynamoContext;
        }

        public async Task<IList<Wallet>> GetWalletsAsync(string userId)
        {
            var wallets = await _dynamoContext.GetByUserIdAsync<Wallet>(userId);
            var transactions = await _dynamoContext.GetByUserIdAsync<WalletTransaction>(userId);

            foreach (var wallet in wallets)
            {
                var coinTransactions = transactions.Where(t => t.Coin == wallet.Coin);
                wallet.Balance = CalculateBalance(coinTransactions);
            }

            return wallets;
        }
        
        private decimal CalculateBalance(IEnumerable<WalletTransaction> transactions)
        {
            var balance = 0m;
            foreach (var transaction in transactions.OrderBy(d => d.Time))
            {
                balance += transaction.Amount;
            }
            return balance;
        }

        public async Task CreateWalletAsync(string userId, string symbol)
        {

        }
    }
}
