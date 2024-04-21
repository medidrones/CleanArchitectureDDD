using CleanArchitecture.Application.Paginations;
using CleanArchitecture.Domain.Abstractions;
using CleanArchitecture.Domain.Vehiculos;

namespace CleanArchitecture.Infrastructure.Repositories;

internal sealed class VehiculoRepository : Repository<Vehiculo, VehiculoId>, IVehiculoRepository, IPaginationVehiculoRepository
{
    public VehiculoRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
    }

    public Task<IReadOnlyList<Vehiculo>> GetAllWhithSpec(ISpecification<Vehiculo, VehiculoId> spec)
    {
        throw new NotImplementedException();
    }
}
