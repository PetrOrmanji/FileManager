using FileManager.Domain.Entities;
using FileManager.Infrastructure.Persistence.Repositories.Base;
using Microsoft.EntityFrameworkCore;

namespace FileManager.Infrastructure.Persistence.Repositories.Entities;

public class FileManagerUserRepository(FileManagerContext fileManagerContext) : BaseFileManagerRepository(fileManagerContext)
{
    public Task<FileManagerUser?> GetAsync(string login)
    {
        return FileManagerContext.Users.FirstOrDefaultAsync(user => user.Login == login);
    }

    public async Task AddAsync(FileManagerUser fileManagerUser)
    {
        await FileManagerContext.Users.AddAsync(fileManagerUser);
    }
}