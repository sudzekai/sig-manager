namespace Presentation.Objects.Responses
{
    public class Error
    {
        public Error(int code, string message)
        {
            Code = code;
            Message = message;
        }

        public int Code { get; set; }
        public string Message { get; set; }
    }
}
