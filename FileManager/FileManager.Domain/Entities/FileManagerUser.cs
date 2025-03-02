namespace FileManager.Domain.Entities;

public class FileManagerUser : EntityBase
{
    public required string Login { get; set; }
    public required string PasswordHash { get; set; }
}
