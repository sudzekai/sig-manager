namespace Shared.Types.Exceptions
{
    public class InternalServerException(string message) : ApplicationException(message)
    {
        public override int Code => 500;
    }
}
