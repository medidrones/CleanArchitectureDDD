using FluentValidation;

namespace CleanArchitecture.Application.Alquileres.ReservarAlquiler;

public class ReservarAlquilerCommandValidator : AbstractValidator<ReservarAlquillerCommand>
{
    public ReservarAlquilerCommandValidator()
    {
        RuleFor(c => c.UserId).NotEmpty();
        RuleFor(c => c.VehiculoId).NotEmpty();
        RuleFor(c => c.FechaInicio).LessThan(c => c.FechaFin);
    }
}
