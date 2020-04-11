using Cell.Cosmos.Configuration;
using Microsoft.Azure.Documents;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace Cell.Cosmos.Storage
{
    public interface ICollectionCreator
    {
        Task<bool> EnsureCreatedAsync<TEntity>(string databaseId, string collectionId, int collectionThroughput, JsonSerializerSettings partitionKeySerializer, IndexingPolicy indexingPolicy = null, ThroughputBehaviour onDatabaseBehaviour = ThroughputBehaviour.UseDatabaseThroughput, UniqueKeyPolicy uniqueKeyPolicy = null) where TEntity : class;
    }
}