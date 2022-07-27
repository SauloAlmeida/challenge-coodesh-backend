using SpaceFlight.API.Constants;
using SpaceFlight.API.Contracts;
using SpaceFlight.API.Model.ViewModel;

namespace SpaceFlight.API.Service
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
