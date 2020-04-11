using System;

namespace Cell.Cosmos.Attributes
{
    [AttributeUsage(AttributeTargets.Property)]
    public class CosmosPartitionKeyAttribute : Attribute
    {
    }
}