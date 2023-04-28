namespace VideoConferencingDemo.Infrastructure.Exceptions;

public class MaxLimitException : Exception
{
    public MaxLimitException(string message) : base(message)
    {
    }
}
