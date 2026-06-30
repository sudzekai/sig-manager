namespace Presentation.Objects.Responses
{
    public class ResponseEnvelope
    {
        public ResponseEnvelope(object? data)
        {
            Success = true;
            Data = data;
        }

        public ResponseEnvelope(Error? error)
        {
            Success = false;
            Error = error;
        }

        public static ResponseEnvelope FromData(object? data) => new(data);

        public static ResponseEnvelope FromError(Shared.Types.Exceptions.ApplicationException ex) => new(new Error(ex.Code, ex.Message));

        public static ResponseEnvelope InternalServerError  => new(new Error(500, "Внутренняя ошибка сервера"));

        public bool Success { get; set; }
        public object? Data { get; set; }
        public Error? Error { get; set; }
    }
}
