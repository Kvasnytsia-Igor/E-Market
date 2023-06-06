using Application.Common.Models;
using FluentValidation;
using FluentValidation.Results;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Application.Common.Behaviours;

public class ValidationBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : class
    where TResponse : class
{
    private readonly IValidator<TRequest>? _validator;

    public ValidationBehaviour(IValidator<TRequest> validator)
    {
        _validator = validator;
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        if (_validator is not null)
        {
            ValidationContext<TRequest> context = new(request);
            ValidationResult validationResult = await _validator.ValidateAsync(context, cancellationToken);
            List<ValidationFailure> failures = validationResult.Errors.ToList();
            if (failures.Any())
            {
                ApiResponse apiResponse = new(StatusCodes.Status500InternalServerError, new
                {
                    Message = "Failed validation",
                    Errors = failures.Select(failure => failure.ErrorMessage).ToArray()
                });
                if (apiResponse is TResponse response)
                {
                    return response;
                }
                throw new ApplicationException();
            }
        }
        return await next();
    }
}
