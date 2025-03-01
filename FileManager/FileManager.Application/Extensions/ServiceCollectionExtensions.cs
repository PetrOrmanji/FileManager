using EnsureThat;
using FileManager.Application.Services;
using Microsoft.Extensions.DependencyInjection;

namespace FileManager.Application.Extensions;

public static class ServiceCollectionExtensions
{
    public static void AddAuthService(
        this IServiceCollection services)
    {
        EnsureArg.IsNotNull(services, nameof(services));

        services.AddScoped<FileManagerAuthService>();
    }
}