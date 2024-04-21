using CleanArchitecture.Application.Abstractions.Messaging;
using CleanArchitecture.Domain.Abstractions;
using CleanArchitecture.Domain.Shared;
using CleanArchitecture.Domain.Vehiculos;

namespace CleanArchitecture.Application.Vehiculos.GetVehiculosKitByPagination;

public record GetVehiculosKitByPaginationQuery 
    : PaginationParams, IQuery<PagedResults<Vehiculo, VehiculoId>>;