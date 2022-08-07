using MongoDB.Driver;
using SpaceFlight.API.Application.Model;

namespace SpaceFlight.API.Core.Contracts.Infrastructure
{
    public interface IContext
    {
        IMongoCollection<Article> Collection { get; set; }
        Task<int> GetNewIdAsync(CancellationToken token = default);
        Task<int> GetLastIdAsync(CancellationToken token = default);
    }
}
