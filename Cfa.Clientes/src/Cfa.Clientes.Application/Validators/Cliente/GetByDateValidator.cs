using Cfa.Clientes.Application.DataBase.Clientes.Queries.GetClientByDate;
using FluentValidation;

namespace Cfa.Clientes.Application.Validators.Cliente;

public class GetByDateValidator : AbstractValidator<GetClientByDateModelInput> 
{
    public GetByDateValidator()
    {
        RuleFor(x => x.FechaInicial).NotEmpty().WithMessage("Debe ingresar una fecha inicial");
        RuleFor(x => x.FechaFinal).NotEmpty().WithMessage("Debe ingresar una fecha Final");
    }
}