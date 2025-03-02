using System.Reflection;
using FileManager.Application.Extensions;
using FileManager.Infrastructure.Extensions;

SetCurrentDirectory();

var builder = WebApplication.CreateBuilder(args);
builder.AddSerilog();

builder.Services.AddJwtOptions(builder.Configuration);
builder.Services.AddDb(builder.Configuration);
builder.Services.AddApiAuthentication(builder.Configuration);
builder.Services.AddAuthService();
builder.Services.AddUserContextService();
builder.Services.AddFileManagerService();
builder.Services.AddControllers().AddNewtonsoftJson();
builder.Services.AddSwagger();
builder.Services.AddMapperProfiles();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
    options.RoutePrefix = string.Empty;
});

app.UseExceptionHandler();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.Run();

static void SetCurrentDirectory()
{
    var entryAssembly = Assembly.GetEntryAssembly();
    if (entryAssembly is null)
    {
        return;
    }

    var assemblyDirectory = Path.GetDirectoryName(entryAssembly.Location);
    if (assemblyDirectory is null)
    {
        return;
    }

    Directory.SetCurrentDirectory(assemblyDirectory);
}