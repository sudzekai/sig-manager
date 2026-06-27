namespace Application.Types.Exceptions
{
    public interface IApplicationException
    {
        int Code { get; }
        string Message { get; }
    }
}
