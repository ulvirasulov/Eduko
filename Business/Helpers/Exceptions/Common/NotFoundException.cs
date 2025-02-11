namespace Business.Helpers.Exceptions.Common;

public class NotFoundException : Exception
{
    public NotFoundException(string entityName) : base($"'{entityName}' tapilmadi.")
    {
    }
}