using MongoDB.Bson;
using SpaceFlight.API.Application.DTO;
using SpaceFlight.API.Application.DTO.ViewModel;
using SpaceFlight.API.Core.Contracts;
using SpaceFlight.API.Core.Contracts.Infrastructure;

namespace SpaceFlight.API.Infrastructure.Persistence
{
    public static class Seed
    {
        private const int LIMIT_ITEMS_PER_REQUEST = 100;

        public static async Task ExecuteAsync(IServiceProvider serviceProvider)
        {
            using IServiceScope scope = serviceProvider.CreateScope();

            var spaceService = scope.ServiceProvider.GetRequiredService<ISpaceFlightApiClient>();

            var db = scope.ServiceProvider.GetRequiredService<IContext>();

            if (db.Collection.CountDocuments(new BsonDocument()) > 0) return;

            int totalArticles = await spaceService.GetTotalArticlesAsync();

            int totalRequests = totalArticles / LIMIT_ITEMS_PER_REQUEST;

            IList<ArticleDTO>[] responseTask = await GetArticlesParallelism(spaceService, totalRequests);

            await InsertAllToDb(db, responseTask);
        }

        private static async Task<IList<ArticleDTO>[]> GetArticlesParallelism(ISpaceFlightApiClient spaceService, int totalRequests)
        {
            List<Task<IList<ArticleDTO>>> tasksToRequest = new();

            for (int index = 0; index <= totalRequests; index++)
            {
                int skip = index * LIMIT_ITEMS_PER_REQUEST;

                ArticleFilterDTO filter = new(LIMIT_ITEMS_PER_REQUEST, skip);

                tasksToRequest.Add(spaceService.GetArticlesAsync(filter));
            }

            return await Task.WhenAll(tasksToRequest);
        }

        private static async Task InsertAllToDb(IContext db, IList<ArticleDTO>[] responseTask)
        {
            List<Task> tasksToInsert = new();

            foreach (var articles in responseTask)
            {
                var models = articles.Select(s => s.ToEntity(s.Id));

                await db.Collection.InsertManyAsync(models);
            }
        }
    }
}
