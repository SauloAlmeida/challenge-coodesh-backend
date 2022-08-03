using SpaceFlight.API.Application.DTO;
using SpaceFlight.API.Application.DTO.ViewModel;
using SpaceFlight.API.Core.Constants;
using SpaceFlight.API.Core.Contracts;
using System.Net;

namespace SpaceFlight.API.Infrastructure.ApiClient
{
    public class SpaceFlightApiClient : ISpaceFlightApiClient
    {
        private readonly HttpClient _httpClient;

        public SpaceFlightApiClient(HttpClient httpClient)
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls | SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11;

            _httpClient = httpClient;
            _httpClient.DefaultRequestHeaders.Accept.Clear();
            _httpClient.DefaultRequestHeaders.Accept.Add(new("application/json"));
        }

        public async Task<IList<ArticleDTO>> GetArticlesAsync(ArticleFilterDTO filter, CancellationToken token = default)
        {
            Thread.Sleep(500);

            var uri = SpaceFlightApiUrlConstants.GetArticles;

            uri = ApplyFilter(uri, filter);

            var result = await _httpClient.GetFromJsonAsync<IList<ArticleDTO>>(uri, token);

            return result ?? new List<ArticleDTO>();
        }

        private static string ApplyFilter(string uri, ArticleFilterDTO filter)
        {
            uri += $"?_limit={filter.Limit}&_sort={filter.Order.ToString().ToLower()}&_start={filter.Skip}";

            if (filter.GreaterThan is not null) uri += $"&{filter.GreaterThan}";

            return uri;
        }

        public async Task<int> GetTotalArticlesAsync(CancellationToken token = default)
        {
            var uri = SpaceFlightApiUrlConstants.GetTotalArticles;

            return await _httpClient.GetFromJsonAsync<int>(uri, token);
        }
    }
}
