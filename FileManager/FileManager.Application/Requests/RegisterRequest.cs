namespace FileManager.Application.Requests;

public record RegisterRequest(
    string Login,
    string Password);