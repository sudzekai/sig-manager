namespace Shared.Types.Exceptions
{
    public class ConflictException(string businessMessage, string message, Exception? inner = null) : ApplicationException(businessMessage, message, inner)
    {
        public override int Code => 409;
    }
}
