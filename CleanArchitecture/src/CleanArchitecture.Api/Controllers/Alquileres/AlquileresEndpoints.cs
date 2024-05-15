using CleanArchitecture.Application.Alquileres.GetAlquiler;
using CleanArchitecture.Application.Alquileres.ReservarAlquiler;
using CleanArchitecture.Domain.Permissions;
using MediatR;

namespace CleanArchitecture.Api.Controllers.Alquileres;

public static class AlquileresEndpoints
{
    public static IEndpointRouteBuilder MapAlquilerEndpoints(this IEndpointRouteBuilder builder)
    {
        builder
            .MapGet("alquileres/{id}", GetAlquiler)
            .RequireAuthorization(PermissionEnum.ReadUser.ToString())
            .WithName(nameof(GetAlquiler));

        builder
            .MapPost("alquileres", ReservaAlquiler)
            .RequireAuthorization(PermissionEnum.WriteUser.ToString());

        return builder;
    }
        
    public static async Task<IResult> GetAlquiler(Guid id, ISender sender, CancellationToken cancellationToken)
    {
        var query = new GetAlquilerQuery(id);
        var resultado = await sender.Send(query, cancellationToken);

        return resultado.IsSuccess ? Results.Ok(resultado.Value) : Results.NotFound();
    }
   
    public static async Task<IResult> ReservaAlquiler(ISender sender, AlquilerReservaRequest request, 
        CancellationToken cancellationToken)
    {
        var command = new ReservarAlquillerCommand(
            request.VehiculoId,
            request.UserId,
            request.StartDate,
            request.EndDate);

        var resultado = await sender.Send(command, cancellationToken);

        if (resultado.IsFailure)
        {
            return Results.BadRequest(resultado.Error);
        }

        return Results.CreatedAtRoute(nameof(GetAlquiler), new {id = resultado.Value}, resultado.Value);
    }
}
