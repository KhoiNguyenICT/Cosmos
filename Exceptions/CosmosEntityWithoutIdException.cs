using System;

namespace Cell.Cosmos.Exceptions
{
    public class CosmosEntityWithoutIdException<TEntity> : Exception
    {
        public CosmosEntityWithoutIdException(TEntity entity) : base($"Unable to resolve Id for cosmos entity of type {entity?.GetType().Name ?? typeof(TEntity).Name}")
        {
        }
    }
}