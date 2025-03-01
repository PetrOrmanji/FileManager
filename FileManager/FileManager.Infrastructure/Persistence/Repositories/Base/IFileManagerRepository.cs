namespace FileManager.Infrastructure.Persistence.Repositories.Base;

public interface IFileManagerRepository : IRepository
{
    FileManagerContext FileManagerContext { get; }
}