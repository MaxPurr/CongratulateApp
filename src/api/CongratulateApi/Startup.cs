using CongratulateApi.DataAccess.Extensions;
using CongratulateApi.DataAccess.Infrastructure;
using CongratulateApi.Application.Extensions;
using CongratulateApi.Domain.Options;
using CongratulateApi.Utils.Extensions;

namespace CongratulateApi;

public class Startup {
    private IConfiguration _configuration;

    public Startup(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public void ConfigureServices(IServiceCollection services)
    {
        services.AddSwaggerGen();
        services.Configure<PostgresOptions>(_configuration.GetSection(nameof(PostgresOptions)));
        services.Configure<CloudinaryOptions>(_configuration.GetSection(nameof(CloudinaryOptions)));
        services.AddCors(options =>
        {
            options.AddPolicy("AllowAllOrigins",
                builder =>
                {
                    builder.AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader();
                });
        }); 
        services.AddMigrations()
            .AddUtils()
            .AddRepositories()
            .AddServices()
            .AddControllers();
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        DapperConfiguration.Apply();
        app.UseCors("AllowAllOrigins");
        app.UseSwagger();
        app.UseSwaggerUI();
        app.UseRouting();
        app.UseEndpoints(endpoints =>
        { 
            endpoints.MapControllers();
        });
    }
}