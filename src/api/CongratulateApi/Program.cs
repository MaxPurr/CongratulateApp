using CongratulateApi.DataAccess.Extensions;
using CongratulateApi.DataAccess.Infrastructure;

namespace CongratulateApi;

public static class Program {
    public static void Main(string[] args)
    {
        CreateHostBuilder(args)
            .Build()
            .MigrateUp()
            .Run();
    }

    private static IHostBuilder CreateHostBuilder(string[] args){
        return Host.CreateDefaultBuilder(args)
            .ConfigureWebHostDefaults(webBuilder => {
                webBuilder.UseStartup<Startup>();
            });
    }
}