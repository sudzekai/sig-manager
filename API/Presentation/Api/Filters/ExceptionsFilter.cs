using Application.Types.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Presentation.Extensions;
using Presentation.Responses;
using Shared.OpenTelemetry.Logging.Extensions;

namespace Presentation.Api.Filters
{
    internal class ExceptionsFilter : IExceptionFilter
    {
        private readonly ILogger<ExceptionsFilter> _logger;

        public ExceptionsFilter(ILogger<ExceptionsFilter> logger)
        {
            _logger = logger;
        }

        public void OnException(ExceptionContext context)
        {
            var ex = context.Exception;

            if (ex is null)
                return;


            if (ex is IApplicationException exception)
            {
                context.Result = ResponseEnvelope.FromError(exception).ToErroredObjectResult();
                _logger.CustomLogError(
                    "Ошибочний ответ на запрос",
                    new()
                    {
                        ["response.code"] = exception.Code,
                        ["response.message"] = exception.Message
                    }
                );
                return;
            }

            context.Result = ResponseEnvelope.InternalServerError.ToErroredObjectResult();
            _logger.CustomLogError(
                "Ошибочний ответ на запрос",
                new()
                {
                    ["response.code"] = 500,
                    ["response.message"] = ResponseEnvelope.InternalServerError.Error?.Message,
                    ["exception.message"] = ex.Message
                }
            );
        }
    }
}
