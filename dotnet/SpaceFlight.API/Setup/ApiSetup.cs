using Microsoft.Extensions.Options;
using MongoDB.Driver;
using Quartz;
using SpaceFlight.API.Core.Contracts;
using SpaceFlight.API.Core.Contracts.Infrastructure;
using SpaceFlight.API.Core.Contracts.Settings;
using SpaceFlight.API.Core.Settings;
using SpaceFlight.API.Infrastructure.ApiClient;
using SpaceFlight.API.Infrastructure.Job;
using SpaceFlight.API.Infrastructure.Persistence;

namespace SpaceFlight.API.Setup
{
    public static class ApiSetup
    {
        public static void Configure(WebApplicationBuilder builder)
        {
            builder.Services.AddControllers();
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
            services.AddSingleton(sp =>
            {
                IDatabaseSettings databaseSettings = sp.GetRequiredService<IDatabaseSettings>();
                MongoClient client = new(databaseSettings.ConnectionString);
                IMongoDatabase db = client.GetDatabase(databaseSettings.DatabaseName);
                return db;
            });
            services.AddSingleton<IContext, Context>();
            services.AddHttpClient<ISpaceFlightApiClient, SpaceFlightApiClient>();

            services.AddQuartz(config =>
            {
                JobKey jobKey = new(nameof(GetNewArticlesJob));

                config.UseMicrosoftDependencyInjectionJobFactory();
                config.AddJob<GetNewArticlesJob>(opt => opt.WithIdentity(jobKey));
                config.AddTrigger(opt => opt
                        .ForJob(jobKey)
                        .WithIdentity($"{nameof(GetNewArticlesJob)}-trigger")
                        .WithCronSchedule(
                                CronScheduleBuilder.DailyAtHourAndMinute(9, 0)
                                                   .InTimeZone(TimeZoneInfo.FindSystemTimeZoneById("E. South America Standard Time"))
                        )
                );
            });

            services.AddQuartzHostedService(q => q.WaitForJobsToComplete = true);
        }
    }
}
