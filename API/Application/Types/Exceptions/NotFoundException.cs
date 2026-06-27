namespace Application.Types.Exceptions
{
    internal class NotFoundException(string message) : Exception(message), IApplicationException
    {
        public int Code => 404;
    }
}
