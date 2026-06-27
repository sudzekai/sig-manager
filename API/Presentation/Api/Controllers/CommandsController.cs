using Application.Objects.Dtos;
using Application.Services.Command;
using Microsoft.AspNetCore.Mvc;
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
        public async Task<string> Process([FromBody] CommandDto body)
        {
            using var activity = ActivitySourceDictionary.Controllers.CommandProcessor.StartActivity("Обработка запроса на выполнение команды");
            return await _commandProcessor.ProcessAsync(body);
        }
    }
}
