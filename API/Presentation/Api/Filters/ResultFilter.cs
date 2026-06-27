using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Presentation.Extensions;
using Presentation.Responses;
using Shared.OpenTelemetry.Logging.Extensions;
using Shared.OpenTelemetry.Tracing.Sources;

namespace Presentation.Api.Filters
{
    public class ResultFilter : IResultFilter
    {
        private readonly ILogger<ResultFilter> _logger;

        public ResultFilter(ILogger<ResultFilter> logger)
        {
            _logger = logger;
        }

        public void OnResultExecuted(ResultExecutedContext context) { }

        public void OnResultExecuting(ResultExecutingContext context)
        {
            using var activity =
                ActivitySourceDictionary.Filters.Response.StartActivity("Сборка ответа клиенту");

            switch (context.Result)
            {
                case ObjectResult { Value: ResponseEnvelope }:
                    break;

                case OkObjectResult ok:
                    var env1 = ResponseEnvelope.FromData(ok.Value);
                    context.Result = env1.ToOkObjectResult();
                    break;

                case ObjectResult obj when obj.Value is not null:
                    var env2 = ResponseEnvelope.FromData(obj.Value);
                    context.Result = env2.ToOkObjectResult();
                    break;

                case OkResult or NoContentResult or EmptyResult:
                    context.Result = new NoContentResult();
                    break;
            }

            _logger.CustomLogInfo(
                "Успешный ответ на запрос",
                new()
                {
                    ["response.code"] = context.HttpContext.Response.StatusCode
                }
            );
        }
    }
}
