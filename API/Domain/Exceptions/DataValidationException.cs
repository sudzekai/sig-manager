namespace Domain.Exceptions
{
    public class DataValidationException(string message) : DomainException(message)
    {
    }
}
