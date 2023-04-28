namespace VideoConferencingDemo.Infrastructure.Exceptions;

public class InvalidLinkException : Exception
{
    public InvalidLinkException(string message) : base(message)
    {
    }
}
