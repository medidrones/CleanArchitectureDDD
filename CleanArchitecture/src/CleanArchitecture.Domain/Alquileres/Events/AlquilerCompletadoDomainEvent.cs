using CleanArchitecture.Domain.Abstractions;

namespace CleanArchitecture.Domain.Alquileres.Events;

public sealed record AlquilerCompletadoDomainEvent(
    AlquilerId AlquilerId) : IDomainEvent;
