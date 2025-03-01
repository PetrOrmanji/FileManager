namespace FileManager.Domain.Requests;

public record LoginRequest(
    string Login,
    string Password);