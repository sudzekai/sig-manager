namespace Shared.Types.Exceptions
{
    public class NotFoundException(string businessMessage, string message, Exception? inner = null) : ApplicationException(businessMessage, message, inner)
    {
        public override int Code => 404;
    }
}
