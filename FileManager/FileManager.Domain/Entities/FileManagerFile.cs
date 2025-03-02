namespace FileManager.Domain.Entities;

public class FileManagerFile : EntityBase
{
    public required string Name { get; set; } = "Untitled file";
    public required string Extension { get; set; } = string.Empty;
    public long SizeInBytes { get; set; }
    public DateTime DownloadDate { get; set; }
    public Guid? FolderId { get; set; }
    public FileManagerFolder? Folder { get; set; }
    public required Guid UserId { get; set; }
    public FileManagerUser? User { get; set; }
    public Guid FileContentId { get; set; }
    public FileManagerFileContent? FileContent { get; set; }
}