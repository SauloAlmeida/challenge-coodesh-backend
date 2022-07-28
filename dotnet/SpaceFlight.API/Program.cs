using SpaceFlight.API.Application.Service;
using SpaceFlight.API.Core.Contracts;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<ISpaceFlightService, SpaceFlightService>();


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapGet("/", () => Results.Ok("Back-end Challenge 2021 - Space Flight News"));

app.MapGet("/articles/api", (ISpaceFlightService service, CancellationToken token) =>
{
    var articles = service.GetArticlesAsync(token);

    return articles;
});

app.Run();
