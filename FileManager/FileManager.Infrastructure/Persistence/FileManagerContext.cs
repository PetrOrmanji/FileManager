using Microsoft.EntityFrameworkCore;
using FileManager.Domain.Entities;

namespace FileManager.Infrastructure.Persistence;

public class FileManagerContext(DbContextOptions<FileManagerContext> options) : DbContext(options)
{
    public DbSet<FileManagerUser> Users => Set<FileManagerUser>();
    public DbSet<FileManagerFolder> Folders => Set<FileManagerFolder>();
    public DbSet<FileManagerFile> Files => Set<FileManagerFile>();
    public DbSet<FileManagerFileContent> FileContents => Set<FileManagerFileContent>();

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

        // FileManagerFolder
        modelBuilder
            .Entity<FileManagerFolder>()
            .HasKey(folder => folder.Id);

        modelBuilder
            .Entity<FileManagerFolder>()
            .HasIndex(folder => folder.Name);

        modelBuilder
            .Entity<FileManagerFolder>()
            .HasMany(folder => folder.SubFolders);

        modelBuilder
            .Entity<FileManagerFolder>()
            .HasMany(folder => folder.Files)
            .WithOne(parentFolder => parentFolder.Folder);

        // FileManagerFile
        modelBuilder
            .Entity<FileManagerFile>()
            .HasKey(file => file.Id);

        modelBuilder
            .Entity<FileManagerFile>()
            .HasIndex(file => file.Name);

        modelBuilder
            .Entity<FileManagerFile>()
            .HasOne(file => file.Folder)
            .WithMany(folder => folder.Files)
            .HasForeignKey(file => file.FolderId)
            .IsRequired(false);

        modelBuilder
            .Entity<FileManagerFile>()
            .HasOne(file => file.FileContent)
            .WithOne(fileContent => fileContent.File)
            .HasForeignKey<FileManagerFileContent>(fileContent => fileContent.FileId)
            .IsRequired();

        // FileManagerFileContent
        modelBuilder
            .Entity<FileManagerFileContent>()
            .HasKey(fileContent => fileContent.Id);

        modelBuilder
            .Entity<FileManagerFileContent>()
            .HasOne(fileContent => fileContent.File)
            .WithOne(file => file.FileContent);
    }
}