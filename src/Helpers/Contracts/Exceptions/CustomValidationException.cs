using FluentValidation;
using FluentValidation.Results;

namespace Shared.Exceptions
{
    internal class CustomValidationException : ValidationException
    {
        public CustomValidationException(IEnumerable<ValidationFailure> errors) : base(errors)
        {
        }
    }
}
