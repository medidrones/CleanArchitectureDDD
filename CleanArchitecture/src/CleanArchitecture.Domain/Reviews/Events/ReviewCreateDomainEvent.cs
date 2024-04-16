using CleanArchitecture.Domain.Abstractions;

namespace CleanArchitecture.Domain.Reviews.Events;

public sealed record ReviewCreateDomainEvent(
    ReviewId ReviewId) : IDomainEvent;
