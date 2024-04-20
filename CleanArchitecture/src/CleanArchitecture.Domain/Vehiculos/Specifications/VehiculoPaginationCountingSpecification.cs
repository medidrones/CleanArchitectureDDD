using CleanArchitecture.Domain.Abstractions;

namespace CleanArchitecture.Domain.Vehiculos.Specifications;

public class VehiculoPaginationCountingSpecification : BaseSpecification<Vehiculo, VehiculoId>
{
    public VehiculoPaginationCountingSpecification(string search)
        : base(x => string.IsNullOrEmpty(search) || x.Modelo == new Modelo(search))
    {
    }
}
