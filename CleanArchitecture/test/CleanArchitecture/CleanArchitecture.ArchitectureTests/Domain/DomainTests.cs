using CleanArchitecture.ArchitectureTests.Infrastructure;
using CleanArchitecture.Domain.Abstractions;
using FluentAssertions;
using NetArchTest.Rules;
using System.Reflection;
using Xunit;

namespace CleanArchitecture.ArchitectureTests.Domain;

public class DomainTests : BaseTest
{
    [Fact]
    public void Entities_ShouldHave_PrivateConstructorNoParameteres()
    {
        IEnumerable<Type> entityTypes = Types.InAssembly(DomainAssembly).That().Inherit(typeof(Entity<>)).GetTypes();

        var erroEntities = new List<Type>();

        foreach (Type entityType in entityTypes)
        {
            ConstructorInfo[] constructores = entityType.GetConstructors(BindingFlags.NonPublic | BindingFlags.Instance);

            if (!constructores.Any(c => c.IsPrivate && c.GetParameters().Length == 0))
            {
                erroEntities.Add(entityType);
            }
        }
        
        erroEntities.Should().BeEmpty();
    }
}
