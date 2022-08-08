using SpaceFlight.API.Application.DTO.ViewModel;
using SpaceFlight.API.Application.Model;

namespace SpaceFlight.API.Core.Contracts.Infrastructure
{
    public interface IArticleRepository
    {
        Task<IList<Article>> GetAsync(int limit, CancellationToken token);
        Task<Article> GetByIdAsync(int id, CancellationToken token);
        Task AddAsync(ArticleDTO dto, CancellationToken token);
        Task UpdateAsync(int id, ArticleDTO dto, CancellationToken token);
        Task DeleteAsync(int id, CancellationToken token);
    }
}
