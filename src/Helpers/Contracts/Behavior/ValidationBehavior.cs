using FluentValidation;
using MediatR;
using Shared.CQRS;
using Shared.Exceptions;

namespace Shared.Behavior
{
    public class ValidationBehavior<TRequest, TResponse>(IEnumerable<IValidator<TRequest>> _validators) : IPipelineBehavior<TRequest, TResponse>
        where TRequest : ICommand<TResponse>
    {
        private readonly IEnumerable<IValidator<TRequest>> validators = _validators;

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next,
            CancellationToken cancellationToken)
        {
            ValidationContext<TRequest> context = new(request);

            FluentValidation.Results.ValidationResult[] result = await Task.WhenAll(validators.Select(p =>
            {
                return p.ValidateAsync(context, cancellationToken);
            }));
            List<FluentValidation.Results.ValidationFailure> errors = result.Where(x =>
            { 
                return x.Errors.Any();
            }).SelectMany
                (x =>
                {
                    return x.Errors;
                }).ToList();

            return errors.Any() ? throw new CustomValidationException(errors) : await next();
        }
    }
}