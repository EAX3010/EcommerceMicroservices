#region

using FluentValidation;
using FluentValidation.Results;
using MediatR;
using Shared.CQRS;
using Shared.Exceptions;

#endregion

namespace Shared.Behavior
{
    public class ValidationBehavior<TRequest, TResponse>(IEnumerable<IValidator<TRequest>> validators)
        : IPipelineBehavior<TRequest, TResponse>
        where TRequest : ICommand<TResponse>
    {
        public async Task<TResponse> Handle(
            TRequest request,
            RequestHandlerDelegate<TResponse> next,
            CancellationToken cancellationToken)
        {
            if (!validators.Any())
            {
                return await next();
            }

            ValidationContext<TRequest> context = new(request);

            ValidationResult[] validationResults = await Task.WhenAll(
                validators.Select(validator => { return validator.ValidateAsync(context, cancellationToken); }));

            List<ValidationFailure> failures = [..validationResults
                .Where(result => { return !result.IsValid; })
                .SelectMany(result => { return result.Errors; })];

            return failures.Count > 0 ? throw new CustomValidationException(failures) : await next();
        }
    }
}