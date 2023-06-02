using FluentValidation;

namespace Application.Laptops.Commands.CreateLaptop;

public class CreateLaptopCommandValidator : AbstractValidator<CreateLaptopCommand>
{
    public CreateLaptopCommandValidator()
    {
        RuleFor(command => command.Brand).Length(3, 255);
        RuleFor(command => command.Series).Length(3, 255);
        RuleFor(command => command.Price).GreaterThan(0);
    }
}
