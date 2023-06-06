using Application.Laptops.Queries.GetLaptopsWithPagination;
using FluentValidation;

namespace CleanArchitecture.Application.TodoItems.Queries.GetTodoItemsWithPagination;

public class GetLaptopsWithPaginationQueryValidator : AbstractValidator<GetLaptopsWithPaginationQuery>
{
    public GetLaptopsWithPaginationQueryValidator()
    {
        RuleFor(query => query.PageNumber).GreaterThanOrEqualTo(1);
        RuleFor(query => query.PageSize).GreaterThanOrEqualTo(1);
    }
}
