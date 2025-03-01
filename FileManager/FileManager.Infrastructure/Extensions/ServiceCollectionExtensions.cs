using System.Text;
using EnsureThat;
using FileManager.Domain.Configuration;
using FileManager.Infrastructure.Persistence;
using FileManager.Infrastructure.Persistence.Repositories.Entities;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

namespace FileManager.Infrastructure.Extensions;

public static class ServiceCollectionExtensions
{
    public static void AddJwtOptions(
        this IServiceCollection services,
        ConfigurationManager configuration)
    {
        services.Configure<JwtOptions>(configuration.GetSection(nameof(JwtOptions)));
    }

    public static void AddSwagger(
        this IServiceCollection services)
    {
        EnsureArg.IsNotNull(services, nameof(services));

        services.AddSwaggerGen(options =>
        {
            options.EnableAnnotations();
            options.SwaggerDoc("v1", new OpenApiInfo
            {
                Version = "v1",
                Title = "File Manager API",
                Description = "An ASP.NET Core Web API for managing file items",
                Contact = new OpenApiContact
                {
                    Name = "Petr",
                    Email = "petrormanji68@mail.ru"
                }
            });
            options.AddSecurityDefinition(
                "Bearer",
                new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Description = "Пожалуйста, вставьте JWT токен в поле в формате: bearer {jwtToken}",
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey
                });
            options.AddSecurityRequirement(
                new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme()
                        {
                            Reference = new OpenApiReference
                            {
                                Id = "Bearer",
                                Type = ReferenceType.SecurityScheme,
                            }
                        },
                        new List<string>()
                    }
                });
        });
    }

    public static void AddApiAuthentication(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        EnsureArg.IsNotNull(services, nameof(services));
        EnsureArg.IsNotNull(configuration, nameof(configuration));

        var jwtOptions = configuration.GetSection(nameof(JwtOptions)).Get<JwtOptions>();

        services
            .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
            {
                options.TokenValidationParameters = new()
                {
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOptions!.SecretKey)),
                };
            });

        services.AddAuthorization();
    }

    public static void AddDb(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        EnsureArg.IsNotNull(services, nameof(services));
        EnsureArg.IsNotNullOrWhiteSpace(configuration["DbConnectionString"], "DbConnectionString");

        services.AddDbContext<FileManagerContext>(options =>
            options.UseSqlServer(configuration["DbConnectionString"]));

        services.AddScoped<FileManagerUserRepository>();
    }
}