namespace Ordering.Domain.Exceptions;

public class DomainException : Exception // Make it public if intended for use outside the assembly
{
    public DomainException(string message)
        : base($"Domain Exception: {message}") // Removed redundant "throws from domain layer"
    {
    }

    public DomainException(string message, Exception innerException) // Add inner exception support
        : base($"Domain Exception: {message}", innerException)
    {
    }
}