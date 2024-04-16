using CleanArchitecture.Domain.Abstractions;

namespace CleanArchitecture.Domain.Alquileres.Events;

public sealed record AlquilerCanceladoDomainEvent(
    AlquilerId AlquilerId) : IDomainEvent;
