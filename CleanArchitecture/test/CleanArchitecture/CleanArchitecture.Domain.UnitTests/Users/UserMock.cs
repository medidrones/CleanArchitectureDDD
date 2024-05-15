using CleanArchitecture.Domain.Users;

namespace CleanArchitecture.Domain.UnitTests.Users;

internal class UserMock
{
    public static readonly Nombre Nombre = new Nombre("Alfonso");
    public static readonly Apellido Apellido = new Apellido("Ramos");
    public static readonly Email Email = new Email("alfonso.ramos@gmail.com");
    public static readonly PasswordHash Password = new ("Test234Test4%");
}
