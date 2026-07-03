using Contracts.Interfaces.Application.Services;
using Contracts.Objects.Dtos;
using Microsoft.AspNetCore.Mvc;
using Presentation.Attributes;
using Shared.Extensions;
using Shared.OpenTelemetry;

namespace Presentation.Api.Controllers
{
    [ApiController]
    [Route("/commands")]
    public class CommandsController(ICommandProcessor commandProcessor)
    {
        private readonly ICommandProcessor _commandProcessor = commandProcessor;

        [HttpPost]
        [ValidateModelState]
        public async Task<string> Process([FromBody] CommandDto body)
        {
            using var activity = Telemetry.Repository.StartRichActivity();

            return await _commandProcessor.ProcessAsync(body);
        }
    }
}
