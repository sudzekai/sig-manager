using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Presentation.Objects.Responses;
using Presentation.Utilities.Extensions;
using Shared.Extensions;
using Shared.OpenTelemetry;

namespace Presentation.Api.Filters
{
    public class ResultFilter(ILogger<ResultFilter> logger) : IResultFilter
    {
        private readonly ILogger<ResultFilter> _logger = logger;

        public void OnResultExecuted(ResultExecutedContext context) { }

        public void OnResultExecuting(ResultExecutingContext context)
        {
            using var activity = Telemetry.Filter.StartRichActivity();

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
        }
    }
}
