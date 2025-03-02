namespace FileManager.Application.DTOs;

public record FolderDto(
    Guid? Id,
    Guid? ParentFolderId,
    string Name,
    IReadOnlyCollection<FolderDto> SubFolders,
    IReadOnlyCollection<FileDto> Files);