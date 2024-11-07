using FluentValidation;
using MediatR;
using Shared.CQRS;

namespace Shared.Validation
{
    public class CommandValidation<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : ICommand<TResponse>
    {
        IEnumerable<IValidator<TRequest>> validators;
        public CommandValidation(IEnumerable<IValidator<TRequest>> _validators)
        {
            validators = _validators;
        }
        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            var context = new ValidationContext<TRequest>(request);

            var result = await Task.WhenAll(validators.Select(p => p.ValidateAsync(context, cancellationToken)));
            var errors = result.Where(x => x.Errors.Any()).SelectMany
                (x => x.Errors).ToList();

            if (errors.Any())
            {
                throw new ValidationException(errors);
            }

            return await next();
        }
    }
}
