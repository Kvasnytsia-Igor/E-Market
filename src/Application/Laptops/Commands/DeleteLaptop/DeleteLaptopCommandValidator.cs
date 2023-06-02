using FluentValidation;

namespace Application.Laptops.Commands.DeleteLaptop;

public class DeleteLaptopCommandValidator : AbstractValidator<DeleteLaptopCommand>
{
    public DeleteLaptopCommandValidator()
    {
        RuleFor(command => command.Id).NotEmpty();
    }
}
