namespace FileManager.Application.DTOs;

public record FileDto(
    Guid Id,
    Guid? FolderId,
    string Name,
    string Extension,
    int SizeInBytes,
    DateTime DownloadDate);
    