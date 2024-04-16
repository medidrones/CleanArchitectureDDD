namespace CleanArchitecture.Domain.Vehiculos;

public record VehiculoId(Guid Value)
{
    public static VehiculoId New() => new(Guid.NewGuid());
}
