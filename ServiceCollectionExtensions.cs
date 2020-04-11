using Microsoft.Extensions.DependencyInjection;
using System;

namespace Cell.Cosmos
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddCosmosStore<TEntity>(this IServiceCollection services,
            CosmosStoreSettings settings, string overriddenCollectionName = "") where TEntity : class
        {
            services.AddSingleton<ICosmosStore<TEntity>>(x => new CosmosStore<TEntity>(settings, overriddenCollectionName));
            return services;
        }

        public static IServiceCollection AddCosmosStore<TEntity>(this IServiceCollection services,
            string databaseName, string endpointUri, string authKey,
            Action<CosmosStoreSettings> settingsAction = null, string overriddenCollectionName = "") where TEntity : class
        {
            return services.AddCosmosStore<TEntity>(databaseName, new Uri(endpointUri), authKey, settingsAction, overriddenCollectionName);
        }

        public static IServiceCollection AddCosmosStore<TEntity>(this IServiceCollection services,
            string databaseName, Uri endpointUri, string authKey, Action<CosmosStoreSettings> settingsAction = null,
            string overriddenCollectionName = "") where TEntity : class
        {
            var settings = new CosmosStoreSettings(databaseName, endpointUri, authKey);
            settingsAction?.Invoke(settings);
            return services.AddCosmosStore<TEntity>(settings, overriddenCollectionName);
        }

        public static IServiceCollection AddCosmosStore<TEntity>(this IServiceCollection services,
            ICosmosClient cosmosClient,
            string databaseName,
            string overriddenCollectionName = "") where TEntity : class
        {
            services.AddSingleton<ICosmosStore<TEntity>>(x => new CosmosStore<TEntity>(cosmosClient, databaseName, overriddenCollectionName));
            return services;
        }
    }
}