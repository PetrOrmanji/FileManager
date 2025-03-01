using EnsureThat;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace FileManager.Infrastructure.Persistence;

internal sealed class FileManagerContextFactory : IDesignTimeDbContextFactory<FileManagerContext>
{
    public FileManagerContext CreateDbContext(string[] args)
    {
        EnsureArg.HasItems(args, nameof(args));

        var optionsBuilder = new DbContextOptionsBuilder<FileManagerContext>();
        optionsBuilder.UseSqlServer(args[0],
            builder => builder.MigrationsAssembly(typeof(FileManagerContextFactory).Assembly.GetName().Name));

        return new FileManagerContext(optionsBuilder.Options);
    }
}