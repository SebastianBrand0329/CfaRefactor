using Cfa.Clientes.Application.DataBase.Clientes.Commands.DeleteClient;
using FluentValidation;

namespace Cfa.Clientes.Application.Validators.Cliente;

public class DeleteClientValidator : AbstractValidator<DeleteClientModel>
{
    public DeleteClientValidator()
    {
        RuleFor(c => c.Codigo).NotEmpty().WithMessage("El código es obligatorio");
    }
}