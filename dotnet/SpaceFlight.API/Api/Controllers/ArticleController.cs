using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using SpaceFlight.API.Application.DTO.ViewModel;
using SpaceFlight.API.Application.Model;
using SpaceFlight.API.Core.Contracts.Infrastructure;

namespace SpaceFlight.API.Api.Controllers
{
    [ApiController]
    [Route("articles")]
    public class ArticleController : ControllerBase
    {
        private readonly IContext _context;

        public ArticleController(IContext context)
        {
            _context = context;
        }

        [HttpGet("/")]
        public IActionResult Info() => Ok("Back-end Challenge 2021 - Space Flight News");

        [HttpGet]
        public async Task<IActionResult> GetAsync(CancellationToken token)
            => Ok(await _context.Collection.Find(_ => true).Limit(5).ToListAsync(token));

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync(int id, CancellationToken token)
            => Ok(await _context.Collection.Find(f => f.Id == id).SingleOrDefaultAsync(token));

        [HttpPost]
        public async Task<IActionResult> InsertAsync([FromBody] ArticleDTO dto, CancellationToken token)
        {
            int newId = await _context.GetNewIdAsync(token);

            var entity = dto.ToEntity(newId);

            await _context.Collection.InsertOneAsync(entity, cancellationToken: token);

            return Created($"/articles/{entity.Id}", entity);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync(int id, [FromBody] ArticleDTO dto, CancellationToken token)
        {
            var filter = Builders<Article>.Filter.Eq(p => p.Id, id);

            await _context.Collection.ReplaceOneAsync(filter, dto.ToEntity(id), cancellationToken: token);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id, CancellationToken token)
        {
            await _context.Collection.DeleteOneAsync(f => f.Id == id, token);

            return NoContent();
        }
    }
}
