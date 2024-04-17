using CleanArchitecture.Domain.Abstractions;
using CleanArchitecture.Domain.Users.Events;

namespace CleanArchitecture.Domain.Users;

public sealed class User : Entity<UserId>
{
    private User()
    {
    }

    private User(
        UserId id, 
        Nombre nombre, 
        Apellido apellido, 
        Email email,
        PasswordHash passwordHash) : base(id)
    {
        Nombre = nombre;
        Apellido = apellido;
        Email = email;
        PasswordHash = passwordHash;
    }

    public Nombre? Nombre { get; private set; }
    public Apellido? Apellido { get; private set; }
    public Email? Email { get; private set; }   
    public PasswordHash? PasswordHash { get; private set; }   

    public static User Create(
        Nombre nombre, 
        Apellido apellido, 
        Email email, 
        PasswordHash passwordHash)
    {
        var user = new User(UserId.New(), nombre, apellido, email, passwordHash);
        user.RaiseDomainEvent(new UserCreateDomainEvent(user.Id!));

        return user;
    }
}
