using SpaceFlight.API.Infrastructure.Persistence;
using SpaceFlight.API.Setup;

var builder = WebApplication.CreateBuilder(args);

ApiSetup.Configure(builder);

var app = builder.Build();

await Seed.ExecuteAsync(app.Services);

app.UseSwagger();

app.UseSwaggerUI();

app.UseHttpsRedirection();

app.MapControllers();

app.Run();

