using Microsoft.Azure.Documents;
using Microsoft.Azure.Documents.Client;
using System.Threading.Tasks;

namespace Cell.Cosmos.Storage
{
    internal class CosmosDatabaseCreator : IDatabaseCreator
    {
        private readonly ICosmosClient _cosmosClient;

        public CosmosDatabaseCreator(ICosmosClient cosmosClient)
        {
            _cosmosClient = cosmosClient;
        }

        public CosmosDatabaseCreator(IDocumentClient documentClient)
        {
            _cosmosClient = new CosmosClient(documentClient);
        }

        public async Task<bool> EnsureCreatedAsync(string databaseId, int? databaseThroughput = null)
        {
            var database = await _cosmosClient.GetDatabaseAsync(databaseId);

            if (database != null) return false;

            var newDatabase = new Database { Id = databaseId };

            database = await _cosmosClient.CreateDatabaseAsync(newDatabase, new RequestOptions
            {
                OfferThroughput = databaseThroughput
            });
            return database != null;
        }
    }
}