using System;

namespace Cell.Cosmos.Exceptions
{
    public class SharedEntityDoesNotImplementException : Exception
    {
        public SharedEntityDoesNotImplementException(Type type) : base($"Shared entity {type.Name} has appropriate attribute but must also implement {nameof(ISharedCosmosEntity)}")
        {
        }
    }
}