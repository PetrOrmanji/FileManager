namespace FileManager.Infrastructure.Persistence.Repositories.Base;

public interface IRepository
{
    Task SaveChangesAsync();
}