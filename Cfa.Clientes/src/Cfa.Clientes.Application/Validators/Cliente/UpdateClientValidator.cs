using Cfa.Clientes.Application.DataBase.Clientes.Commands.UpdateCliente;
using FluentValidation;

namespace Cfa.Clientes.Application.Validators.Cliente;

public class UpdateClientValidator : AbstractValidator<UpdateClientModel>
{
    public UpdateClientValidator()
    {
        RuleFor(c => c.Codigo).NotEmpty().WithMessage("El código es obligatorio");

        RuleFor(c => c.TipoDocumento)
        .NotEmpty().WithMessage("El tipo de documento es obligatorio.")
        .Must(tipo => tipo == "CC" || tipo == "TI" || tipo == "RC")
        .WithMessage("El tipo de documento debe ser 'CC', 'TI' o 'RC'.");

        RuleFor(c => c.NumeroDocumento)
       .NotEmpty().WithMessage("El número de documento es obligatorio.")
       .Must(numero => numero >= 1 && numero <= 99999999999).WithMessage("El número de documento debe tener hasta 11 dígitos.");

        RuleFor(c => c.FechaNacimiento)
        .NotEmpty().WithMessage("La fecha de nacimiento es obligatoria.")
        .Must((cliente, fecha) => ValidarEdadPorTipoDocumento(cliente.TipoDocumento, fecha))
        .WithMessage("El tipo de documento no es válido para la edad del cliente.");

    }

    private bool ValidarEdadPorTipoDocumento(string tipoDocumento, DateTime fechaNacimiento)
    {
        var edad = DateTime.Now.Year - fechaNacimiento.Year;
        if (DateTime.Now.DayOfYear < fechaNacimiento.DayOfYear)
            edad--;

        return (tipoDocumento == "RC" && edad <= 7) ||
               (tipoDocumento == "TI" && edad >= 8 && edad <= 17) ||
               (tipoDocumento == "CC" && edad > 17);
    }

}