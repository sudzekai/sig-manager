namespace Shared.Types.Exceptions
{
    public abstract class ApplicationException(string message) : Exception(message)
    {
        public abstract int Code { get; }
    }
}
