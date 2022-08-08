using Microsoft.AspNetCore.Mvc;
using SpaceFlight.API.Application.DTO.ViewModel;
using SpaceFlight.API.Core.Contracts.Infrastructure;

namespace SpaceFlight.API.Api.Controllers
{
    [ApiController]
    [Route("articles")]
    public class ArticleController : ControllerBase
    {
        private readonly IArticleRepository _repository;

        public ArticleController(IArticleRepository repository)
        {
            _repository = repository;
        }

        [HttpGet("/")]
        public IActionResult Info() => Ok("Back-end Challenge 2021 - Space Flight News");

        [HttpGet]
        public async Task<IActionResult> GetAsync(CancellationToken token)
            => Ok(await _repository.GetAsync(limit: 5, token));

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync(int id, CancellationToken token)
            => Ok(await _repository.GetByIdAsync(id, token));

        [HttpPost]
        public async Task<IActionResult> InsertAsync([FromBody] ArticleDTO dto, CancellationToken token)
        {
            int id = await _repository.AddAsync(dto, token);

            return Created($"/articles/{id}", null);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync(int id, [FromBody] ArticleDTO dto, CancellationToken token)
        {
            await _repository.UpdateAsync(id, dto, token);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id, CancellationToken token)
        {
            await _repository.DeleteAsync(id, token);

            return NoContent();
        }
    }
}
