using Amazon.DynamoDBv2.DataModel;

namespace MasternodePools.Data.Entities
{
    [DynamoDBTable("transactions", true)]
    public class WalletTransaction
    {
        [DynamoDBHashKey("txid")]
        public string TxId { get; set; }

        [DynamoDBRangeKey]
        public string UserId { get; set; }

        [DynamoDBProperty]
        public string Address { get; set; }

        [DynamoDBProperty]
        public string Category { get; set; }

        [DynamoDBProperty]
        public decimal Amount { get; set; }

        [DynamoDBProperty]
        public int Time { get; set; }

        [DynamoDBProperty]
        public int TimeReceived { get; set; }

        [DynamoDBProperty]
        public int Confirmations { get; set; }

        [DynamoDBProperty]
        public string Coin { get; set; }
    }
}
