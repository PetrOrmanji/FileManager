namespace FileManager.Domain.Entities;

public class EntityBase
{
    public Guid Id { get; } = Guid.NewGuid();
}