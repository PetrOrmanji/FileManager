using Microsoft.EntityFrameworkCore;
using FileManager.Domain.Entities;

namespace FileManager.Infrastructure.Persistence;

public class FileManagerContext(DbContextOptions<FileManagerContext> options) : DbContext(options)
{
    public DbSet<FileManagerUser> Users => Set<FileManagerUser>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        //FileManagerUser
        modelBuilder
            .Entity<FileManagerUser>()
            .HasKey(user => user.Id);

        modelBuilder
            .Entity<FileManagerUser>()
            .HasIndex(user => user.Login)
            .IsUnique();
    }
}