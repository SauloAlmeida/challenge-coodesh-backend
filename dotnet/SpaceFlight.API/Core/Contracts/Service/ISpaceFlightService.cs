using SpaceFlight.API.Application.Model.ViewModel;

namespace SpaceFlight.API.Core.Contracts
{
    public interface ISpaceFlightService
    {
        Task<IList<ArticeViewModel>> GetArticlesAsync(CancellationToken token);
    }
}
