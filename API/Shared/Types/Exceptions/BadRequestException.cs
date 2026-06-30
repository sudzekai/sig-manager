namespace Shared.Types.Exceptions
{
    public class BadRequestException(string message) : ApplicationException(message)
    {
        public override int Code => 400;
    }
}
