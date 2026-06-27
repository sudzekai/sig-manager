namespace Application.Types.Exceptions
{
    internal class ExitApplicationException(string message) : Exception(message), ICriticalException
    {
    }
}
