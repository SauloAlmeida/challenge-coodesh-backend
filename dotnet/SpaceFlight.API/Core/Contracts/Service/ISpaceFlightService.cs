using SpaceFlight.API.Application.DTO.ViewModel;

namespace SpaceFlight.API.Core.Contracts
{
    public interface ISpaceFlightService
    {
        Task<IList<ArticleDTO>> GetArticlesAsync(CancellationToken token);
    }
}
