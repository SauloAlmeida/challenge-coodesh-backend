using MongoDB.Driver;
using SpaceFlight.API.Application.DTO.ViewModel;
using SpaceFlight.API.Application.Model;
using SpaceFlight.API.Core.Contracts.Infrastructure;

namespace SpaceFlight.API.Infrastructure.Persistence
{
    public class ArticleRepository : IArticleRepository
    {
        private readonly IContext _context;

        public ArticleRepository(IContext context)
        {
            _context = context;
        }

        public async Task<int> AddAsync(ArticleDTO dto, CancellationToken token)
        {
            int newId = await _context.GetNewIdAsync(token);

            var entity = dto.ToEntity(newId);

            await _context.Collection.InsertOneAsync(entity, cancellationToken: token);

            return newId;
        }

        public async Task DeleteAsync(int id, CancellationToken token)
        {
            await _context.Collection.DeleteOneAsync(f => f.Id == id, token);
        }

        public async Task<IList<Article>> GetAsync(int limit, CancellationToken token)
        {
            return await _context.Collection.Find(_ => true).Limit(limit).ToListAsync(token);
        }

        public async Task<Article> GetByIdAsync(int id, CancellationToken token)
        {
           return await _context.Collection.Find(f => f.Id == id).SingleOrDefaultAsync(token);
        }

        public async Task UpdateAsync(int id, ArticleDTO dto, CancellationToken token)
        {
            var filter = Builders<Article>.Filter.Eq(p => p.Id, id);

            await _context.Collection.ReplaceOneAsync(filter, dto.ToEntity(id), cancellationToken: token);
        }
    }
}
