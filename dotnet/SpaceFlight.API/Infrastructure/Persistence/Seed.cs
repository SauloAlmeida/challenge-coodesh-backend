using MongoDB.Bson;
using SpaceFlight.API.Application.DTO;
using SpaceFlight.API.Application.DTO.ViewModel;
using SpaceFlight.API.Core.Contracts;
using SpaceFlight.API.Core.Contracts.Infrastructure;

namespace SpaceFlight.API.Infrastructure.Persistence
{
    public static class Seed
    {
        private const int LIMIT_ITEMS_PER_REQUEST = 200;

        public static async Task ExecuteAsync(IServiceProvider serviceProvider)
        {
            using IServiceScope scope = serviceProvider.CreateScope();

            var spaceService = scope.ServiceProvider.GetRequiredService<ISpaceFlightApiClient>();

            var db = scope.ServiceProvider.GetRequiredService<IDatabase>();

            if (db.Collection.CountDocuments(new BsonDocument()) > 0) return;

            int totalArticles = await spaceService.GetTotalArticlesAsync();

            int totalRequests = totalArticles / LIMIT_ITEMS_PER_REQUEST;

            IList<ArticleDTO>[] responseTask = await GetArticlesParalellism(spaceService, totalRequests);

            await InsertParalellism(db, responseTask);
        }

        private static async Task InsertParalellism(IDatabase db, IList<ArticleDTO>[] responseTask)
        {
            List<Task> tasksToInsert = new();

            foreach (var articles in responseTask)
            {
                var models = articles.Select(s => s.ToEntity(s.Id));

                tasksToInsert.Add(db.Collection.InsertManyAsync(models));
            }

            await Task.WhenAll(tasksToInsert);
        }

        private static async Task<IList<ArticleDTO>[]> GetArticlesParalellism(ISpaceFlightApiClient spaceService, int totalRequests)
        {
            List<Task<IList<ArticleDTO>>> tasksToRequest = new();

            for (int index = 0; index < totalRequests; index++)
            {
                int skip = index * LIMIT_ITEMS_PER_REQUEST;

                ArticleFilterDTO filter = new(LIMIT_ITEMS_PER_REQUEST, skip);

                tasksToRequest.Add(spaceService.GetArticlesAsync(filter));
            }

            return await Task.WhenAll(tasksToRequest);
        }
    }
}
