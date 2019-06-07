using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using Amazon.DynamoDBv2.DocumentModel;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MasternodePools.Data.Context
{
    public class DynamoContext : DynamoDBContext, IDynamoDBContext
    {
        public DynamoContext(IAmazonDynamoDB client)
            : base(client)
        {
        }

        public async Task<IList<T>> GetByUserIdAsync<T>(string userId)
        {
            var condition = new ScanCondition("UserId", ScanOperator.Equal, userId);
            var transactions = await ScanAsync<T>(new ScanCondition[] { condition }).GetRemainingAsync();
            return transactions;
        }
    }
}
