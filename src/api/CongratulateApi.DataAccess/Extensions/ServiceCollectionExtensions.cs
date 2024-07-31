using CongratulateApi.DataAccess.Migrations;
using CongratulateApi.DataAccess.Repositories;
using FluentMigrator.Runner;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using CongratulateApi.Domain.Options;
using CongratulateApi.Domain.Repositories.Interfaces;

namespace CongratulateApi.DataAccess.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddMigrations(this IServiceCollection services)
    {
        return services.AddFluentMigratorCore()
            .ConfigureRunner(rb => rb.AddPostgres()
                .WithGlobalConnectionString(s =>
                {
                    var cfg = s.GetRequiredService<IOptions<PostgresOptions>>();
                    return cfg.Value.ConnectionString;
                })
                .ScanIn(typeof(InitSchema).Assembly).For.Migrations()
            )
            .AddLogging(lb => lb.AddFluentMigratorConsole());
    }

    public static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        return services.AddSingleton<IPersonRepository, PersonRepository>()
            .AddSingleton<IImageRepository, ImageRepository>();
    }
}