using CleanArchitecture.Domain.Abstractions;

namespace CleanArchitecture.Domain.UnitTests.Infrastructure;

public abstract class BaseTest
{
    public static T AssertDomainEventWasPublished<T>(IEntity entity) where T : IDomainEvent
    {
        var domainEvent = entity.GetDomainEvents().OfType<T>().SingleOrDefault();

        if (domainEvent is null)
        {
            throw new Exception($"{typeof(T).Name} no fue publicado");
        }

        return domainEvent!;
    }
}
