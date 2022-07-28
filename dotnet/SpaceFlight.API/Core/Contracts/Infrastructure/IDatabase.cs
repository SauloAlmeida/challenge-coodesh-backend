using MongoDB.Driver;
using SpaceFlight.API.Application.Model;

namespace SpaceFlight.API.Core.Contracts.Infrastructure
{
    public interface IDatabase
    {
        IMongoCollection<Article> Collection { get; set; }
    }
}
