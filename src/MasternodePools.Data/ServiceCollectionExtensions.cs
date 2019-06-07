using Amazon;
using Amazon.DynamoDBv2;
using Amazon.Runtime;
using MasternodePools.Data.Context;
using MasternodePools.Data.Entities;
using MasternodePools.Data.Services;
using MasternodePools.Data.Services.Abstraction;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace MasternodePools.Data
{
    public static class ServiceCollectionExtensions
    {
        public static void AddMasternodeEntities(this IServiceCollection services,
            IConfiguration config)
        {
            var connectionString = config.GetConnectionString("MasternodePools");
            services.AddDbContext<UserContext>(options =>
            {
                options.UseSqlServer(connectionString);
            });

            services.AddTransient<IEntityService<User>, UserService>();
        }

        public static void AddDynamoDb(this IServiceCollection services,
            IConfiguration config, string accessKey, string secretKey, string region)
        {
            var awsOptions = config.GetAWSOptions();
            awsOptions.Credentials = new BasicAWSCredentials(accessKey, secretKey);
            awsOptions.Region = RegionEndpoint.GetBySystemName(region);
            var client = awsOptions.CreateServiceClient<IAmazonDynamoDB>();

            services.AddTransient(provider => new DynamoContext(client));

            services.AddTransient<IWalletService, WalletService>();
        }
    }
}
