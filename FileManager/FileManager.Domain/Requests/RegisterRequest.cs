namespace FileManager.Domain.Requests;

public record RegisterRequest(
    string Login,
    string Password);