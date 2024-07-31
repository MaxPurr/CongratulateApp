using CongratulateApi.Domain.Providers.Interfaces;
using CongratulateApi.Utils.Providers;
using Microsoft.Extensions.DependencyInjection;

namespace CongratulateApi.Utils.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddUtils(this IServiceCollection services)
    {
        return services.AddSingleton<IDateOnlyProvider, DateOnlyProvider>();
    }
}