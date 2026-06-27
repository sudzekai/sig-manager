namespace Application.Types.Exceptions
{
    internal class BadRequestException(string message) : Exception(message), IApplicationException
    {
        public int Code => 400;
    }
}
