using MongoDB.Driver;
using SpaceFlight.API.Application.Model;
using SpaceFlight.API.Core.Contracts.Infrastructure;
using SpaceFlight.API.Core.Contracts.Settings;

namespace SpaceFlight.API.Infrastructure.Persistence
{
    public class Context : IContext
    {
        const string ArticlesCollectionName = "article";

        public IMongoCollection<Article> Collection { get; set; }

        public Context(IMongoDatabase mongoDatabase)
        {
            Collection = mongoDatabase.GetCollection<Article>(ArticlesCollectionName);
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
