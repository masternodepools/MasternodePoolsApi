using Amazon.DynamoDBv2.DataModel;

namespace MasternodePools.Data.Entities
{
    [DynamoDBTable("wallets", true)]
    public class Wallet
    {
        [DynamoDBHashKey]
        public string UserId { get; set; }

        [DynamoDBProperty]
        public string Coin { get; set; }

        [DynamoDBProperty]
        public double CreatedAt { get; set; }

        public decimal Balance { get; set; }
    }
}
