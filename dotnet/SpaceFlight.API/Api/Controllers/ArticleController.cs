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
        private readonly IDatabase _db;

        public ArticleController(IDatabase db)
        {
            _db = db;
        }

        [HttpGet("/")]
        public IActionResult Info() => Ok("Back-end Challenge 2021 - Space Flight News");

        [HttpGet("")]
        public async Task<IActionResult> Get(CancellationToken token)
            => Ok(await _db.Collection.Find(_ => true).Limit(5).ToListAsync(token));

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id, CancellationToken token)
            => Ok(await _db.Collection.Find(f => f.Id == id).SingleOrDefaultAsync(token));

        [HttpPost()]
        public async Task<IActionResult> Insert([FromBody] ArticleDTO dto, CancellationToken token)
        {
            int newId = await _db.GetNewIdAsync(token);

            var entity = dto.ToEntity(newId);

            await _db.Collection.InsertOneAsync(entity, cancellationToken: token);

            return Created($"/articles/{entity.Id}", entity);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] ArticleDTO dto, CancellationToken token)
        {
            var filter = Builders<Article>.Filter.Eq(p => p.Id, id);

            await _db.Collection.ReplaceOneAsync(filter, dto.ToEntity(id), cancellationToken: token);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id, CancellationToken token)
        {
            await _db.Collection.DeleteOneAsync(f => f.Id == id, token);

            return NoContent();
        }
    }
}
