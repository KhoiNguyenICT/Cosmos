using Cell.Cosmos.Configuration;
using Cell.Cosmos.Extensions;
using Microsoft.Azure.Documents;
using Microsoft.Azure.Documents.Client;
using Newtonsoft.Json;
using System;
using System.Threading.Tasks;

namespace Cell.Cosmos.Storage
{
    internal class CosmosCollectionCreator : ICollectionCreator
    {
        private readonly ICosmosClient _cosmosClient;

        public CosmosCollectionCreator(ICosmosClient cosmosClient)
        {
            _cosmosClient = cosmosClient;
        }

        public CosmosCollectionCreator(IDocumentClient documentClient)
        {
            _cosmosClient = new CosmosClient(documentClient);
        }

        public async Task<bool> EnsureCreatedAsync<TEntity>(
            string databaseId,
            string collectionId,
            int collectionThroughput,
            JsonSerializerSettings partitionKeySerializer,
            IndexingPolicy indexingPolicy = null,
            ThroughputBehaviour onDatabaseBehaviour = ThroughputBehaviour.UseDatabaseThroughput,
            UniqueKeyPolicy uniqueKeyPolicy = null) where TEntity : class
        {
            var collectionResource = await _cosmosClient.GetCollectionAsync(databaseId, collectionId);
            var databaseHasOffer = (await _cosmosClient.GetOfferV2ForDatabaseAsync(databaseId)) != null;

            if (collectionResource != null)
                return true;

            var newCollection = new DocumentCollection
            {
                Id = collectionId,
                IndexingPolicy = indexingPolicy ?? CosmosConstants.DefaultIndexingPolicy,
                UniqueKeyPolicy = uniqueKeyPolicy ?? CosmosConstants.DefaultUniqueKeyPolicy
            };

            SetPartitionKeyDefinitionForCollection(typeof(TEntity), newCollection, partitionKeySerializer);

            var finalCollectionThroughput = databaseHasOffer ? onDatabaseBehaviour == ThroughputBehaviour.DedicateCollectionThroughput ? (int?)collectionThroughput : null : collectionThroughput;

            newCollection = await _cosmosClient.CreateCollectionAsync(databaseId, newCollection, new RequestOptions
            {
                OfferThroughput = finalCollectionThroughput
            });

            return newCollection != null;
        }

        private static void SetPartitionKeyDefinitionForCollection(Type entityType, DocumentCollection collection, JsonSerializerSettings serializerSettings)
        {
            var partitionKey = entityType.GetPartitionKeyDefinitionForEntity(serializerSettings);

            if (partitionKey != null)
                collection.PartitionKey = partitionKey;
        }
    }
}