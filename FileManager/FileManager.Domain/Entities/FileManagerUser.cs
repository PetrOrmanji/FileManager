namespace FileManager.Domain.Entities;

public class FileManagerUser : EntityBase
{
    public string Login { get; set; } = string.Empty;
    public string PasswordHash { get; set; } = string.Empty;
}
