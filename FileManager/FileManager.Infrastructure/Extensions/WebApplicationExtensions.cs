using EnsureThat;
using FileManager.Infrastructure.Middleware;
using Microsoft.AspNetCore.Builder;

namespace FileManager.Infrastructure.Extensions;

public static class WebApplicationExtensions
{
    public static void UseExceptionHandler(this WebApplication app)
    {
        EnsureArg.IsNotNull(app, nameof(app));

        app.UseMiddleware<GlobalExceptionHandlerMiddleware>();
    }
}