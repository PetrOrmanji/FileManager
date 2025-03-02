namespace FileManager.Application.Requests;

public record AddFolderRequest(
    string Name,
    Guid? ParentFolderId);
