using CleanArchitecture.Application.Abstractions.Data;
using CleanArchitecture.Application.Abstractions.Messaging;
using CleanArchitecture.Domain.Abstractions;
using Dapper;

namespace CleanArchitecture.Application.Alquileres.GetAlquiler;

internal sealed class GetAlquilerQueryHandler : IQueryHandler<GetAlquilerQuery, AlquilerResponse>
{
    private readonly ISqlConnectionFactory _sqlConnectionFactory;

    public GetAlquilerQueryHandler(ISqlConnectionFactory sqlConnectionFactory)
    {
        _sqlConnectionFactory = sqlConnectionFactory;
    }

    public async Task<Result<AlquilerResponse>> Handle(GetAlquilerQuery request, CancellationToken cancellationToken)
    {
        using var connection = _sqlConnectionFactory.CreateConnection();

        var sql = """
            SELECT 
                id AS Id,
                vehiculo_id AS VehiculoId,
                user_id AS UserId,
                status AS Status,
                precio_por_periodo AS PrecioAlquiler,
                precio_por_periodo_tipo_moneda AS TipoMonedaAlquiler,
                precio_mantenimiento AS PrecioMantenimiento,
                precio_mantenimiento_tipo_moneda AS TipoMonedaMantenimiento,
                precio_accesorios AS AccesoriosPrecio,
                precio_accesorios_tipo_moneda AS TipoMonedaAccesorio,
                precio_total AS PrecioTotal,
                precio_total_tipo_moneda AS PrecioTotalTipoMoneda,
                duracion_inicio AS DuracionInicio,
                duracion_final AS DuracionFinal,
                fecha_creacion AS FechaCreacion
            FROM alquileres WHERE id=@AlquilerId
            """;

        var alquiler = await connection.QueryFirstOrDefaultAsync<AlquilerResponse>(sql, new { request.AlquilerId });

        return alquiler!;
    }
}
