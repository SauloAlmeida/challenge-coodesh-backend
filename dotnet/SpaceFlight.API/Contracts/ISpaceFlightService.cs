using SpaceFlight.API.Model.ViewModel;

namespace SpaceFlight.API.Contracts
{
    public interface ISpaceFlightService
    {
        Task<ArticeViewModel> GetArticlesAsync(CancellationToken token);
    }
}
