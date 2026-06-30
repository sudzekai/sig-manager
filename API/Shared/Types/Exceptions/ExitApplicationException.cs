namespace Shared.Types.Exceptions
{
    public class ExitApplicationException(string message) : Exception(message), ICriticalException
    {
    }
}
