using FluentValidation;

namespace Application.Laptops.Commands.UpdateLaptop;

public class UpdateLaptopCommandValidator : AbstractValidator<UpdateLaptopCommand>
{
    public UpdateLaptopCommandValidator()
    {
        RuleFor(command => command.Id).NotEmpty();

        RuleFor(command => command.LaptopDTO)
            .ChildRules(dto =>
            {
                dto.RuleFor(user => user.Brand)
                .NotEmpty()
                .Matches("^[A-Za-z]+$")
                .Length(3, 255);

                dto.RuleFor(user => user.Series)
                .NotEmpty()
                .Matches("^[A-Za-z]+$")
                .Length(3, 255);

                dto.RuleFor(user => user.Price)
                .GreaterThan(0);
            });
    }
}