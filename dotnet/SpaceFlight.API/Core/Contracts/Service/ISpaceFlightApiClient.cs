using SpaceFlight.API.Application.DTO;
using SpaceFlight.API.Application.DTO.ViewModel;

namespace SpaceFlight.API.Core.Contracts
{
    public interface ISpaceFlightApiClient
    {
        Task<IList<ArticleDTO>> GetArticlesAsync(ArticleFilterDTO filter, CancellationToken token = default);
        Task<int> GetTotalArticlesAsync(CancellationToken token = default);
    }
}
