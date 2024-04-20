using CleanArchitecture.Domain.Abstractions;

namespace CleanArchitecture.Domain.Vehiculos;

public interface IVehiculoRepository
{
    Task<Vehiculo?> GetByIdAsync(VehiculoId id, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<Vehiculo>> GetAllWhithSpec(ISpecification<Vehiculo, VehiculoId> spec);
    Task<int> CountAsync(ISpecification<Vehiculo, VehiculoId> spec);
}
