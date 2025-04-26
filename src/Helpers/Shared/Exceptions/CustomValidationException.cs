#region

using FluentValidation;
using FluentValidation.Results;

#endregion

namespace Shared.Exceptions
{
    internal class CustomValidationException(IEnumerable<ValidationFailure> errors) : ValidationException(errors)
    {
    }
}