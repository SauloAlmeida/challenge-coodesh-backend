using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using SpaceFlight.API.Application.DTO.ViewModel;
using SpaceFlight.API.Application.Model;
using SpaceFlight.API.Core.Contracts.Infrastructure;

namespace SpaceFlight.API.Api
{
    public static class ArticleRoutes
    {
        public static void Handler(WebApplication app)
        {
            app.MapGet("/", () => Results.Ok("Back-end Challenge 2021 - Space Flight News"));

            app.MapGet("/articles", async (IDatabase db, CancellationToken token)
                => await db.Collection.Find(_ => true).ToListAsync(token));

            app.MapGet("/articles/{id}", async (int id, IDatabase db, CancellationToken token)
                => await db.Collection.Find(f => f.Id == id).SingleOrDefaultAsync(token));

            app.MapPost("/articles", async ([FromBody] ArticleDTO dto, IDatabase db, CancellationToken token) =>
            {
                var entity = dto.ToEntity();

                entity.Id = await db.GetNewIdAsync(token);

                await db.Collection.InsertOneAsync(entity, cancellationToken: token);

                Results.Created($"/articles/{entity.Id}", entity);
            });

            app.MapPut("/articles/{id}", async (int id, [FromBody] ArticleDTO dto, IDatabase db, CancellationToken token) =>
            {
                var filter = Builders<Article>.Filter.Eq(p => p.Id, id);

                await db.Collection.ReplaceOneAsync(filter, dto.ToEntity(id), cancellationToken: token);

                Results.NoContent();
            });

            app.MapDelete("/articles/{id}", async (long id, IDatabase db, CancellationToken token) =>
            {
                await db.Collection.DeleteOneAsync(f => f.Id == id, token);
                Results.NoContent();
            });
        }
    }
}
