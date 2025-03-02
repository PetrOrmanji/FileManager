using AutoMapper;
using EnsureThat;
using FileManager.Application.DTOs;
using FileManager.Application.Requests;
using FileManager.Infrastructure.Extensions;
using FileManager.Infrastructure.Persistence.Repositories.Entities;

namespace FileManager.Application.Services;

public class FileManagerService
{
    private readonly FileManagerRepository _fileManagerRepository;
    private readonly IMapper _mapper;

    public FileManagerService(FileManagerRepository fileManagerRepository, IMapper mapper)
    {
        EnsureArg.IsNotNull(fileManagerRepository, nameof(fileManagerRepository));
        EnsureArg.IsNotNull(mapper, nameof(mapper));

        _fileManagerRepository = fileManagerRepository;
        _mapper = mapper;
    }

    public async Task<FileDto?> GetFile(Guid fileId, Guid? folderId, Guid userId)
    {
        var fileEntity = await _fileManagerRepository.GetFileAsync(fileId, folderId, userId);
        if (fileEntity == null)
        {
            return null;
        }

        return _mapper.Map<FileDto>(fileEntity);
    }

    public async Task AddFile(AddFileRequest request, Guid userId)
    {
        await _fileManagerRepository.AddAndSaveFileAsync(
            Path.GetFileName(request.File.FileName),
            Path.GetExtension(request.File.FileName), 
            request.File.Length,
            request.FolderId,
            await request.File.ToBytesAsync(),
            userId);
    }

    public async Task DeleteFile(DeleteFileRequest request, Guid userId)
    {
        await _fileManagerRepository.DeleteFileAndSaveAsync(request.FileId, request.FolderId, userId);
    }

    public async Task<FolderDto?> GetFolder(Guid? folderId, Guid userId)
    {
        if (folderId.HasValue)
        {
            var folderEntity = await _fileManagerRepository.GetFolderAsync(folderId.Value, userId);
            if (folderEntity == null)
            {
                return null;
            }

            return _mapper.Map<FolderDto>(folderEntity);
        }

        var rootFolders = _mapper.Map<List<FolderDto>>(await _fileManagerRepository.GetRootFoldersAsync(userId));
        var rootFiles = _mapper.Map<List<FileDto>>(await _fileManagerRepository.GetRootFilesAsync(userId));

        return new FolderDto(null, null, "Default", rootFolders, rootFiles);
    }

    public async Task AddFolder(AddFolderRequest request, Guid userId)
    {
        await _fileManagerRepository.AddAndSaveFolderAsync(request.Name, request.ParentFolderId, userId);
    }

    public async Task DeleteFolder(DeleteFolderRequest request, Guid userId)
    {
        await _fileManagerRepository.DeleteFolderAndSaveAsync(request.FolderId, userId);
    }
}