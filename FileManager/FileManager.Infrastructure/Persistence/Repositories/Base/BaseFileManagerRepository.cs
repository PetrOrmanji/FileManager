using EnsureThat;

namespace FileManager.Infrastructure.Persistence.Repositories.Base;

public abstract class BaseFileManagerRepository : IFileManagerRepository
{
    public FileManagerContext FileManagerContext { get; }

    protected BaseFileManagerRepository(FileManagerContext fileManagerContext)
    {
        EnsureArg.IsNotNull(fileManagerContext, nameof(fileManagerContext));

        FileManagerContext = fileManagerContext;
    }

    public Task SaveChangesAsync()
    {
        return FileManagerContext.SaveChangesAsync();
    }
}