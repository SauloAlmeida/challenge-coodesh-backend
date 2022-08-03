using Quartz;
using SpaceFlight.API.Application.DTO;
using SpaceFlight.API.Core.Contracts;
using SpaceFlight.API.Core.Contracts.Infrastructure;

namespace SpaceFlight.API.Infrastructure.Job
{
    public class GetNewArticlesJob : IGetNewArticlesJob
    {
        private readonly ISpaceFlightApiClient _apiClient;
        private readonly IDatabase _db;

        public GetNewArticlesJob(ISpaceFlightApiClient apiClient, IDatabase db)
        {
            _apiClient = apiClient;
            _db = db;
        }

        public Task Execute(IJobExecutionContext context)
        { 
            ArticleFilterDTO filter = new(limit: 100);

            int lastId = _db.GetLastIdAsync().GetAwaiter().GetResult();

            if (lastId == 0) return Task.CompletedTask;

            filter.AddGreaterThan(ArticleField.Id, lastId.ToString());

            var articles = _apiClient.GetArticlesAsync(filter).GetAwaiter().GetResult();

            if (articles.Any()) 
                _db.Collection.InsertManyAsync(articles.Select(s => s.ToEntity(s.Id))).GetAwaiter().GetResult();

            return Task.CompletedTask;
        }
    }
}
