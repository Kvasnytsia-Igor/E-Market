using Application.Laptops.Queries.GetLaptopById;
using FluentValidation;

namespace ValidationWithoutExceptions.Users.Queries.GetUserById;

public class GetLaptopByIdValidator : AbstractValidator<GetLaptopByIdQuery>
{
    public GetLaptopByIdValidator()
    {
        RuleFor(a => a.Id).NotEmpty();
    }
}
