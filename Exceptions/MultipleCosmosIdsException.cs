using System;

namespace Cell.Cosmos.Exceptions
{
    public class MultipleCosmosIdsException : Exception
    {
        public MultipleCosmosIdsException(string message) : base(message)
        {
        }
    }
}