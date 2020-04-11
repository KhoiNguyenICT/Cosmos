using Newtonsoft.Json;

namespace Cell.Cosmos
{
    public interface ISharedCosmosEntity
    {
        [JsonProperty(nameof(CosmosEntityName))]
        string CosmosEntityName { get; set; }
    }
}