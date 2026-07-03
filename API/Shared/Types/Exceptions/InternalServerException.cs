namespace Shared.Types.Exceptions
{
    public class InternalServerException(string businessMessage, string message, Exception? inner = null) : ApplicationException(businessMessage, message, inner)
    {
        public override int Code => 500;
    }
}
