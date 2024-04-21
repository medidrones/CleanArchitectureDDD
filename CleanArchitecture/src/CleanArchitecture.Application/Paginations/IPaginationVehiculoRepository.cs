using CleanArchitecture.Domain.Abstractions;
using CleanArchitecture.Domain.Vehiculos;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace CleanArchitecture.Application.Paginations;

public interface IPaginationVehiculoRepository
{
    Task<PagedResults<Vehiculo, VehiculoId>> GetPaginationAsync(
        Expression<Func<Vehiculo, bool>>? predicate,
        Func<IQueryable<Vehiculo>, IIncludableQueryable<Vehiculo, object>> includes,
        int page, int pageSize, string orderBy, bool ascending, bool disableTracking = true);
}
