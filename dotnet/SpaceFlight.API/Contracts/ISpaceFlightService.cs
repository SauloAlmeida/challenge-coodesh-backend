using SpaceFlight.API.Model.ViewModel;

namespace SpaceFlight.API.Contracts
{
    public interface ISpaceFlightService
    {
        Task<IList<ArticeViewModel>> GetArticlesAsync(CancellationToken token);
    }
}
