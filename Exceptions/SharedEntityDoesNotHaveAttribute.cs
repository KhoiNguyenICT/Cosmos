using Cell.Cosmos.Attributes;
using System;

namespace Cell.Cosmos.Exceptions
{
    public class SharedEntityDoesNotHaveAttribute : Exception
    {
        public SharedEntityDoesNotHaveAttribute(Type type) : base($"Shared entity {type.Name} implements {nameof(ISharedCosmosEntity)} but must also have the {nameof(SharedCosmosCollectionAttribute)}")
        {
        }
    }
}