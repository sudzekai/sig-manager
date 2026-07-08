using Microsoft.AspNetCore.Mvc;
using Presentation.Internal.Objects.Responses;

namespace Presentation.Internal.Utilities.Extensions
{
    internal static class ObjectResultBuilder
    {
        public static ObjectResult ToObjectResult(this ResponseEnvelope response, int statusCode) => new(response) { StatusCode = statusCode };

        public static ObjectResult ToCreatedObjectResult(this ResponseEnvelope response) => response.ToObjectResult(StatusCodes.Status201Created);

        public static ObjectResult ToOkObjectResult(this ResponseEnvelope response) => response.ToObjectResult(StatusCodes.Status200OK);

        public static ObjectResult ToErroredObjectResult(this ResponseEnvelope response) => response.ToObjectResult(response.Error is null ? 500 : response.Error.Code);
    }
}
