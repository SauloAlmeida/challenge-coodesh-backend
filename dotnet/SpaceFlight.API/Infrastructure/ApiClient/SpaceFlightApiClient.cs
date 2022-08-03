using SpaceFlight.API.Application.DTO;
using SpaceFlight.API.Application.DTO.ViewModel;
using SpaceFlight.API.Core.Constants;
using SpaceFlight.API.Core.Contracts;
using System.Net;

namespace SpaceFlight.API.Infrastructure.ApiClient
{
    public class SpaceFlightApiClient : ISpaceFlightApiClient
    {
        public readonly HttpClient httpClient;

        public SpaceFlightApiClient(HttpClient httpClient)
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls | SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11;
            
            this.httpClient = httpClient;
            this.httpClient.DefaultRequestHeaders.Accept.Clear();
            this.httpClient.DefaultRequestHeaders.Accept.Add(new("application/json"));
        }

        public async Task<IList<ArticleDTO>> GetArticlesAsync(ArticleFilterDTO filter, CancellationToken token = default)
        {
            var uri = SpaceFlightApiUrlConstants.GetArticles;

            uri = ApplyFilter(uri, filter);

            var result = await httpClient.GetFromJsonAsync<IList<ArticleDTO>>(uri, token);

            return result ?? new List<ArticleDTO>();
        }

        private static string ApplyFilter(string uri, ArticleFilterDTO filter)
            => uri += $"?_limit={filter.Limit}&_sort={filter.Order.ToString().ToLower()}&_start={filter.Skip}";

        public async Task<int> GetTotalArticlesAsync(CancellationToken token = default)
        {
            var uri = SpaceFlightApiUrlConstants.GetTotalArticles;

            return await httpClient.GetFromJsonAsync<int>(uri, token);
        }
    }
}
