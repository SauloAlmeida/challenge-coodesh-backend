using SpaceFlight.API.Application.DTO.ViewModel;
using SpaceFlight.API.Core.Constants;
using SpaceFlight.API.Core.Contracts;

namespace SpaceFlight.API.Application.Service
{
    public class SpaceFlightService : ISpaceFlightService
    {     
        public async Task<IList<ArticleDTO>> GetArticlesAsync(CancellationToken token)
        {
            var uri = SpaceFlightApiUrlConstants.GetArticles;

            var result = await new HttpClient().GetFromJsonAsync<IList<ArticleDTO>>(uri, token);

            return result ?? new List<ArticleDTO>();
        }
    }
}
