using FluentValidation;
using FluentValidation.Results;

namespace Shared.Exceptions
{
    internal class CustomValidationException(IEnumerable<ValidationFailure> errors) : ValidationException(errors)
    {
    }
}