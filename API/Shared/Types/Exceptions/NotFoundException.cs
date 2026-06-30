namespace Shared.Types.Exceptions
{
    public class NotFoundException(string message) : ApplicationException(message)
    {
        public override int Code => 404;
    }
}
