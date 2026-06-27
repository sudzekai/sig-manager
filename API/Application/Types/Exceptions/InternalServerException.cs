namespace Application.Types.Exceptions
{
    internal class InternalServerException(string message) : Exception(message), IApplicationException
    {
        public int Code => 500;
    }
}
