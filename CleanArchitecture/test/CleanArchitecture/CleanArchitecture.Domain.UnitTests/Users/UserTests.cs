using CleanArchitecture.Domain.Roles;
using CleanArchitecture.Domain.UnitTests.Infrastructure;
using CleanArchitecture.Domain.Users;
using CleanArchitecture.Domain.Users.Events;
using FluentAssertions;
using Xunit;

namespace CleanArchitecture.Domain.UnitTests.Users;

public class UserTests : BaseTest
{
    [Fact]
    public void Create_Should_SetPropertyValues()
    {        
        var user = User.Create(UserMock.Nombre, UserMock.Apellido, UserMock.Email, UserMock.Password);
    
        user.Nombre.Should().Be(UserMock.Nombre);
        user.Apellido.Should().Be(UserMock.Apellido);
        user.Email.Should().Be(UserMock.Email);
        user.PasswordHash.Should().Be(UserMock.Password);
    }

    [Fact]
    public void Create_Should_RaiseUserCreateDomainEvent()
    {        
        var user = User.Create(UserMock.Nombre, UserMock.Apellido, UserMock.Email, UserMock.Password);        
        var domainEvent = AssertDomainEventWasPublished<UserCreateDomainEvent>(user);

        domainEvent!.UserId.Should().Be(user.Id);
    }

    [Fact]
    public void Create_Should_AddRegisterRoleToUser()
    {
        var user = User.Create(UserMock.Nombre, UserMock.Apellido, UserMock.Email, UserMock.Password);

        user.Roles.Should().Contain(Role.Cliente);
    }
}
