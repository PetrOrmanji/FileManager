using EnsureThat;
using Microsoft.Extensions.DependencyInjection;

namespace FileManager.Infrastructure.Extensions;

public static class MvcBuilderExtensions
{
    public static void AddNewtonsoftJson(IMvcBuilder builder)
    {
        EnsureArg.IsNotNull(builder, nameof(builder));

        builder.AddNewtonsoftJson(options =>
            options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);
    }
}