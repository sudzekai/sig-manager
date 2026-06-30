using Contracts.Interfaces.Application.Services;
using Contracts.Objects.Dtos.Models.Requests;
using Microsoft.AspNetCore.Mvc;
using Presentation.Attributes;
using Shared.OpenTelemetry.Tracing.Sources;

namespace Presentation.Api.Controllers
{
    [ApiController]
    [Route("/commands")]
    public class CommandsController
    {
        private readonly ICommandProcessor _commandProcessor;

        public CommandsController(ICommandProcessor commandProcessor)
        {
            _commandProcessor = commandProcessor;
        }

        [HttpPost]
        [ValidateModelState]
        public async Task<string> Process([FromBody] CommandDto body)
        {
            using var activity = ActivitySourceDictionary.Controllers.CommandProcessor.StartActivity("Обработка запроса на выполнение команды");

            return await _commandProcessor.ProcessAsync(body);
        }
    }
}
