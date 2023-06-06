using FluentValidation;

namespace Application.Laptops.Commands.UpdateLaptop;

public class UpdateLaptopCommandValidator : AbstractValidator<UpdateLaptopCommand>
{
    public UpdateLaptopCommandValidator()
    {
        RuleFor(command => command.Id).NotEqual(Guid.Empty);
        RuleFor(command => command.LaptopDTO)
            .ChildRules(dto =>
            {
                dto.RuleFor(user => user.Brand).Length(3, 255);
                dto.RuleFor(user => user.Series).Length(3, 255);
                dto.RuleFor(user => user.Price).GreaterThan(0);
            });
    }
}