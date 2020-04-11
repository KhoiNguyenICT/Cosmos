using System.Threading.Tasks;

namespace Cell.Cosmos.Storage
{
    public interface IDatabaseCreator
    {
        Task<bool> EnsureCreatedAsync(string databaseName, int? databaseThroughput = null);
    }
}