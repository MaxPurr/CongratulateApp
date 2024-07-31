using CongratulateApi.Application.Services;
using CongratulateApi.Domain.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace CongratulateApi.Application.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddServices(this IServiceCollection services)
    {
        return services.AddSingleton<IPersonService, PersonService>();
    }
}