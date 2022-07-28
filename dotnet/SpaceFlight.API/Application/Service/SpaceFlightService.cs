using SpaceFlight.API.Core.Constants;
using SpaceFlight.API.Core.Contracts;
using SpaceFlight.API.Application.Model.ViewModel;

namespace SpaceFlight.API.Application.Service
{
    public class SpaceFlightService : ISpaceFlightService
    {     
        public async Task<IList<ArticeViewModel>> GetArticlesAsync(CancellationToken token)
        {
            var uri = SpaceFlightApiUrlConstants.GetArticles;

            var result = await new HttpClient().GetFromJsonAsync<IList<ArticeViewModel>>(uri, token);

            return result ?? new List<ArticeViewModel>();
        }
    }
}
