namespace CleanArchitecture.Application.Users.RegisterUsers;

public record RegisterUserRequest(
    string Email, string Nombre, string Apellidos, string Password);
