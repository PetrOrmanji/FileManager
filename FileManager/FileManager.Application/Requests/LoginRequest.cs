namespace FileManager.Application.Requests;

public record LoginRequest(
    string Login,
    string Password);