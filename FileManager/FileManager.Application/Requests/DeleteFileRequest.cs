namespace FileManager.Application.Requests;

public record DeleteFileRequest(
    Guid FileId,
    Guid? FolderId);
