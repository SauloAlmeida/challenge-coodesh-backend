using MongoDB.Driver;
using SpaceFlight.API.Application.Model;
using SpaceFlight.API.Core.Contracts.Infrastructure;
using SpaceFlight.API.Core.Contracts.Settings;

namespace SpaceFlight.API.Infrastructure.Persistence
{
    public class Database : IDatabase
    {
        public IMongoCollection<Article> Collection { get; set; }

        public Database(IDatabaseSettings settings)
        {
            var client = new MongoClient(@settings.ConnectionString);

            var mongoDatabase = client.GetDatabase(settings.DatabaseName);

            Collection = mongoDatabase.GetCollection<Article>(settings.CollectionName);
        }

        public async Task<int> GetNewIdAsync(CancellationToken token = default)
        {
            var lastArticleId = await GetLastIdAsync(token);

            return lastArticleId + 1;
        }

        public async Task<int> GetLastIdAsync(CancellationToken token = default)
        {
            var lastArticle = await Collection.Find(_ => true).SortByDescending(f => f.Id).Limit(1).FirstOrDefaultAsync(token);

            return lastArticle?.Id ?? 0;
        }
    }
}
