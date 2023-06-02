using Application.Common.Models;
using FluentValidation;
using FluentValidation.Results;
using MediatR;

namespace Application.Common.Behaviours;

public class ValidationBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : class
    where TResponse : class
{
    private readonly IEnumerable<IValidator<TRequest>> _validators;

    public ValidationBehaviour(IEnumerable<IValidator<TRequest>> validators)
    {
        _validators = validators;
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        if (_validators.Any())
        {
            ValidationContext<TRequest> context = new(request);
            ValidationResult[] validationResults = await Task.WhenAll(
                _validators.Select(v => v.ValidateAsync(context, cancellationToken)));
            List<ValidationFailure> failures = validationResults
                .Where(r => r.Errors.Any())
                .SelectMany(r => r.Errors)
                .ToList();
            if (failures.Any())
            {
                if (new ApiResponse400(
                    failures.Select(
                        failure => failure.ErrorMessage)
                    .ToList()) is TResponse response)
                {
                    return response;
                }
                throw new ApplicationException();
            }
        }
        return await next();
    }
}
