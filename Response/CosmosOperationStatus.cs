namespace Cell.Cosmos.Response
{
    public enum CosmosOperationStatus
    {
        Success,
        RequestRateIsLarge,
        ResourceNotFound,
        PreconditionFailed,
        Conflict
    }
}