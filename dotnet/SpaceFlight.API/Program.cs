using MongoDB.Driver;
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

app.MapGet("/articles", async (IDatabase db, CancellationToken token) =>
{
    var articles = await db.Collection.Find(_ => true).ToListAsync(token);

    return articles;
});

app.Run();
