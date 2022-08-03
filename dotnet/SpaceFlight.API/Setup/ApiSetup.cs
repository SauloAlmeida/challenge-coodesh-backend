using Microsoft.Extensions.Options;
using MongoDB.Driver;
using SpaceFlight.API.Core.Contracts;
using SpaceFlight.API.Core.Contracts.Infrastructure;
using SpaceFlight.API.Core.Contracts.Settings;
using SpaceFlight.API.Core.Settings;
using SpaceFlight.API.Infrastructure.ApiClient;
using SpaceFlight.API.Infrastructure.Persistence;

namespace SpaceFlight.API.Setup
{
    public static class ApiSetup
    {
        public static void Configure(WebApplicationBuilder builder)
        {
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.SetupSettings(builder.Configuration);
            builder.Services.SetupInfrastructure();
        }

        static void SetupSettings(this IServiceCollection services, ConfigurationManager configuration)
        {
            services.Configure<DatabaseSettings>(configuration.GetSection(nameof(DatabaseSettings)));
            services.AddSingleton<IDatabaseSettings>(x => x.GetRequiredService<IOptions<DatabaseSettings>>().Value);
        }

        static void SetupInfrastructure(this IServiceCollection services)
        {
            services.AddSingleton<IMongoClient>(c => new MongoClient(c.GetService<IDatabaseSettings>().ConnectionString));
            services.AddScoped(c => c.GetService<IMongoClient>().StartSession());
            services.AddScoped<IDatabase, Database>();
            services.AddHttpClient<ISpaceFlightApiClient, SpaceFlightApiClient>();
        }
    }
}
