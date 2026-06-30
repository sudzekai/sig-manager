namespace Domain.Exceptions
{
    public class DataUnchangedException(string message) : DomainException(message)
    {
    }
}
