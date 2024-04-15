using Bogus;
using CleanArchitecture.Application.Abstractions.Data;
using CleanArchitecture.Domain.Vehiculos;
using Dapper;

namespace CleanArchitecture.Api.Extensions;

public static class SeedDataExtensions
{
    public static void SeedData(this IApplicationBuilder app)
    {
        using var scope = app.ApplicationServices.CreateScope();
        var sqlConnectionFactory = scope.ServiceProvider.GetRequiredService<ISqlConnectionFactory>();
        using var connection = sqlConnectionFactory.CreateConnection();

        var faker = new Faker();

        List<object> vehiculos = new();

        for (var i = 0; i < 100; i++)
        {
            vehiculos.Add(new
            {
                Id = Guid.NewGuid(),
                Vin = faker.Vehicle.Vin(),
                Modelo = faker.Vehicle.Model(),
                Pais = faker.Address.Country(),
                Departamento = faker.Address.State(),
                Provincia = faker.Address.County(),
                Ciudad = faker.Address.City(),
                Calle = faker.Address.StreetAddress(),
                PrecioMonto = faker.Random.Decimal(1000, 20000),
                PrecioTipoMoneda = "USD",
                PrecioMantenimiento = faker.Random.Decimal(100, 200),
                PrecioMantenimientoTipoMoneda = "USD",
                Accesorios = new List<int> { (int)Accesorio.Wifi, (int)Accesorio.AppleCar },
                FechaUltima = DateTime.MinValue

            });
        }

        const string sql = """
            INSERT INTO public.vehiculos
                (id, vin, modelo, direccion_pais, direccion_departamento, direccion_provincia, direccion_ciudad, direccion_calle, precio_monto, precio_tipo_moneda, mantenimiento_monto, mantenimiento_tipo_moneda, accesorios, fecha_ultima_alquiler)
                values(@id, @Vin,@Modelo,@Pais, @Departamento, @Provincia, @Ciudad, @Calle, @PrecioMonto, @PrecioTipoMoneda, @PrecioMantenimiento, @PrecioMantenimientoTipoMoneda, @Accesorios, @FechaUltima)
        """;

        connection.Execute(sql, vehiculos);
    }
}
