namespace Business.Helpers.Exceptions.Common;

public class BusinessException : Exception
{
    public BusinessException(string message) : base(message)
    {
    }
}