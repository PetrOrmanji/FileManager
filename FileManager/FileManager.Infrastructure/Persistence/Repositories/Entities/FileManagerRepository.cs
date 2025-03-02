using FileManager.Domain.Entities;
using FileManager.Infrastructure.Persistence.Repositories.Base;
using Microsoft.EntityFrameworkCore;

namespace FileManager.Infrastructure.Persistence.Repositories.Entities;

public class FileManagerRepository(FileManagerContext fileManagerContext) : BaseFileManagerRepository(fileManagerContext)
{
    #region Files

    public Task<FileManagerFile?> GetFileAsync(Guid fileId, Guid? folderId, Guid userId, bool includeFileContent = true)
    {
        IQueryable<FileManagerFile> query = FileManagerContext.Files;

        if (includeFileContent)
        {
            query = query.Include(file => file.FileContent);
        }

        return query.FirstOrDefaultAsync(fileInfo => fileInfo.Id == fileId && fileInfo.FolderId == folderId && fileInfo.UserId == userId);
    }

    public Task<List<FileManagerFile>> GetRootFilesAsync(Guid userId)
    {
        return FileManagerContext.Files
            .Where(folder => folder.FolderId == null && folder.UserId == userId)
            .ToListAsync();
    }

    public async Task AddAndSaveFileAsync(string name, string extension, long sizeInBytes, Guid? folderId, byte[] fileData, Guid userId)
    {
        var file = new FileManagerFile
        {
            Id = Guid.NewGuid(),
            Name = name,
            Extension = extension,
            SizeInBytes = sizeInBytes,
            DownloadDate = DateTime.Now,
            FolderId = folderId,
            UserId = userId
        };

        await FileManagerContext.Files.AddAsync(file);
        await SaveChangesAsync(); 

        var fileContent = new FileManagerFileContent
        {
            FileId = file.Id,
            Content = fileData
        };

        await FileManagerContext.FileContents.AddAsync(fileContent);
        await SaveChangesAsync();

        file.FileContentId = fileContent.Id;
        await SaveChangesAsync();
    }

    public async Task DeleteFileAndSaveAsync(Guid fileId, Guid? folderId, Guid userId)
    {
        var fileToDelete = await GetFileAsync(fileId, folderId, userId);
        if (fileToDelete != null)
        {
            var fileContentToDelete = fileToDelete.FileContent;
            if (fileContentToDelete != null)
            {
                FileManagerContext.FileContents.Remove(fileContentToDelete);
            }

            FileManagerContext.Files.Remove(fileToDelete);

            await SaveChangesAsync();
        }
    }

    #endregion

    #region Folders

    public Task<FileManagerFolder?> GetFolderAsync(Guid folderId, Guid userId)
    {
        return FileManagerContext.Folders
            .Include(folder => folder.SubFolders)
            .Include(folder => folder.Files)
            .Include(folder => folder.ParentFolder)
            .Include(folder => folder.User)
            .FirstOrDefaultAsync(folder => folder.Id == folderId && folder.UserId == userId);
    }

    public Task<List<FileManagerFolder>> GetRootFoldersAsync(Guid userId)
    {
        return FileManagerContext.Folders
            .Include(folder => folder.SubFolders)
            .Include(folder => folder.Files)
            .Where(folder =>  folder.ParentFolderId == null && folder.UserId == userId)
            .ToListAsync();
    }

    public async Task AddAndSaveFolderAsync(string name, Guid? parentFolderId, Guid userId)
    {
        var folder = new FileManagerFolder
        {
            Name = name,
            UserId = userId,
            ParentFolderId = parentFolderId
        };

        await FileManagerContext.Folders.AddAsync(folder);
        await SaveChangesAsync();
    }

    public async Task DeleteFolderAndSaveAsync(Guid folderId, Guid userId)
    {
        var folderToDelete = await GetFolderAsync(folderId, userId);
        if (folderToDelete == null)
            return;

        var filesToDelete = folderToDelete.Files.ToList();
        var subFoldersToDelete = folderToDelete.SubFolders.ToList();

        foreach (var file in filesToDelete)
        {
            await DeleteFileAndSaveAsync(file.Id, file.FolderId, userId);
        }

        foreach (var subFolder in subFoldersToDelete)
        {
            await DeleteFolderAndSaveAsync(subFolder.Id, userId);
        }

        FileManagerContext.Folders.Remove(folderToDelete);

        await SaveChangesAsync();
    }


    #endregion
}