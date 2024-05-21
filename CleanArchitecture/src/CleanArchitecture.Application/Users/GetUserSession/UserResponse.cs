namespace CleanArchitecture.Application.Users.GetUserSession;

public sealed class UserResponse
{
    public Guid Id { get; init; }
    public string? Email { get; set; }
    public string? Nombre { get; set; }
    public string? Apellido { get; set; }
}
