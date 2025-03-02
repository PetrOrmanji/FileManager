namespace FileManager.Domain.Entities;

public class FileManagerFileContent : EntityBase
{
    public required Guid FileId { get; set; }
    public FileManagerFile? File { get; set; }
    public required byte[] Content { get; set; }
}