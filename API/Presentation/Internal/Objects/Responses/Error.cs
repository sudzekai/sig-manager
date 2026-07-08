namespace Presentation.Internal.Objects.Responses
{
    public class Error(int code, string message)
    {
        public int Code { get; init; } = code;
        public string Message { get; init; } = message;
    }
}
