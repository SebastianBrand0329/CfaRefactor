using Cfa.Clientes.Application.DataBase.Clientes.Commands.CrearCliente;
using FluentValidation;

namespace Cfa.Clientes.Application.Validators.Cliente;

public class ClienteValidator : AbstractValidator<CreateClientModel>
{
    public ClienteValidator()
    {
        RuleFor(c => c.TipoDocumento)
                .NotEmpty().WithMessage("El tipo de documento es obligatorio.")
                .Must(tipo => tipo == "CC" || tipo == "TI" || tipo == "RC")
                .WithMessage("El tipo de documento debe ser 'CC', 'TI' o 'RC'.");

        RuleFor(c => c.NumeroDocumento)
        .NotEmpty().WithMessage("El número de documento es obligatorio.")
        .Must(numero => numero >= 1 && numero <= 99999999999).WithMessage("El número de documento debe tener hasta 11 dígitos.");

        RuleFor(c => c.Email)
            .NotEmpty().WithMessage("El correo electrónico es obligatorio.")
            .Matches(@"^[\w\.-]+@[a-zA-Z\d\.-]+\.[a-zA-Z]{2,6}$")
            .WithMessage("El formato del correo electrónico no es válido.");


        RuleFor(c => c.Nombres)
            .NotEmpty().WithMessage("El nombre es obligatorio.")
            .MaximumLength(30).WithMessage("El nombre debe tener un máximo de 30 caracteres.");

        RuleFor(c => c.Apellido1)
            .NotEmpty().WithMessage("El primer apellido es obligatorio.")
            .MaximumLength(30).WithMessage("El primer apellido debe tener un máximo de 30 caracteres.");

        RuleFor(c => c.Apellido2)
            .MaximumLength(30).WithMessage("El segundo apellido debe tener un máximo de 30 caracteres.");

        RuleFor(c => c.Genero)
            .NotEmpty().WithMessage("El género es obligatorio.")
            .Matches("^(F|M)$").WithMessage("El género debe ser 'F' o 'M'.");


        RuleFor(c => c.FechaNacimiento)
            .NotEmpty().WithMessage("La fecha de nacimiento es obligatoria.")
            .Must((cliente, fecha) => ValidarEdadPorTipoDocumento(cliente.TipoDocumento, fecha))
            .WithMessage("El tipo de documento no es válido para la edad del cliente.");

        RuleFor(c => c.Direcciones)
            .NotEmpty().WithMessage("Debe ingresar al menos una dirección.");


        RuleFor(c => c.Telefonos)
            .NotEmpty().WithMessage("Debe ingresar al menos un teléfono.");
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