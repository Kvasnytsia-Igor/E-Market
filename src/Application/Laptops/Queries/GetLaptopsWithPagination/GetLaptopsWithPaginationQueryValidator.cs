using Application.Laptops.Queries.GetLaptopsWithPagination;
using FluentValidation;

namespace CleanArchitecture.Application.TodoItems.Queries.GetTodoItemsWithPagination;

public class GetLaptopsWithPaginationQueryValidator : AbstractValidator<GetLaptopsWithPaginationQuery>
{
    public GetLaptopsWithPaginationQueryValidator()
    {
        RuleFor(query => query.PageNumber)
            .GreaterThanOrEqualTo(1)
            .WithMessage("{PropertyName} at least greater than or equal to 1.");

        RuleFor(query => query.PageSize)
            .Cascade(CascadeMode.Stop)
            .GreaterThanOrEqualTo(1)
            .WithMessage("{PropertyName} at least greater than or equal to 1.")
            .Must((query, pageSize) => (query.PageNumber - 1) * pageSize > 1)
            .WithMessage("{PropertyName} is too big.");
    }
}
