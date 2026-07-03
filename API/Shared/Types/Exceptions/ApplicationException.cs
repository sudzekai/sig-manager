namespace Shared.Types.Exceptions
{
    public abstract class ApplicationException(string businessMessage, string message, Exception? inner = null) : Exception(message, inner)
    {
        public string BusinessMessage { get; } = businessMessage;
        public abstract int Code { get; }
    }
}
