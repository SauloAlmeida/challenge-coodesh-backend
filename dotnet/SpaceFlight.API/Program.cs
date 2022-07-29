using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using SpaceFlight.API.Application.DTO.ViewModel;
using SpaceFlight.API.Application.Model;
using SpaceFlight.API.Core.Contracts.Infrastructure;
using SpaceFlight.API.Setup;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

ApiSetup.Configure(builder);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapGet("/", () => Results.Ok("Back-end Challenge 2021 - Space Flight News"));

app.MapGet("/articles", async (IDatabase db, CancellationToken token)
    => await db.Collection.Find(_ => true).ToListAsync(token));

app.MapGet("/articles/{id}", async (long id, IDatabase db, CancellationToken token)
    => await db.Collection.Find(f => f.Id == id).SingleOrDefaultAsync(token));

app.MapPost("/articles", async ([FromBody] ArticleDTO dto, IDatabase db, CancellationToken token) =>
{
    var entity = dto.ToEntity();

    entity.Id = await db.GetNewIdAsync(token);

    await db.Collection.InsertOneAsync(entity, cancellationToken: token);

    Results.Created($"/articles/{entity.Id}", entity);
});

app.Run();
