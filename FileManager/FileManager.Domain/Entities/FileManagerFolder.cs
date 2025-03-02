namespace FileManager.Domain.Entities;

public class FileManagerFolder : EntityBase
{
    public string Name { get; set; } = "Untitled folder";
    public Guid? ParentFolderId { get; set; }
    public FileManagerFolder? ParentFolder { get; set; }
    public required Guid UserId { get; set; }
    public FileManagerUser? User { get; set; }

    public ICollection<FileManagerFolder> SubFolders { get; } = new List<FileManagerFolder>();
    public ICollection<FileManagerFile> Files { get; } = new List<FileManagerFile>();
}