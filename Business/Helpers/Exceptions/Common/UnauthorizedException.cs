namespace Business.Helpers.Exceptions.Common;

public class UnauthorizedException : Exception
{
    public UnauthorizedException(string message) : base(message)
    {
    }
}