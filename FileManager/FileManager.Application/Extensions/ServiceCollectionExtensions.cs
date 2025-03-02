using EnsureThat;
using FileManager.Application.MapProfiles;
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

    public static void AddFileManagerService(
        this IServiceCollection services)
    {
        EnsureArg.IsNotNull(services, nameof(services));

        services.AddScoped<FileManagerService>();
    }

    public static void AddMapperProfiles(
        this IServiceCollection services)
    {
        EnsureArg.IsNotNull(services, nameof(services));

        services.AddAutoMapper(
            typeof(FileProfile),
            typeof(FolderProfile));
    }

    public static void AddUserContextService(this IServiceCollection services)
    {
        EnsureArg.IsNotNull(services, nameof(services));

        services.AddHttpContextAccessor();
        services.AddScoped<UserContextService>();
    }
}