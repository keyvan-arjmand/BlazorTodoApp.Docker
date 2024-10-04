
using Microsoft.Extensions.DependencyInjection;

namespace ToDo.Infrastructure;

public static class ConfigureServices
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
    {
        // services.AddScoped<IFileService, FileService>();
        // services.AddScoped<IUnitOfWork, UnitOfWork>();
        return services;
    }
}