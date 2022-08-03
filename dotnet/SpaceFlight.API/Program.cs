using SpaceFlight.API.Api;
using SpaceFlight.API.Infrastructure.Persistence;
using SpaceFlight.API.Setup;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

ApiSetup.Configure(builder);

var app = builder.Build();

await Seed.ExecuteAsync(app.Services);

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

ArticleRoutes.Handler(app);

app.Run();

