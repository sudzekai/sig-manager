using Contracts.Interfaces.Application.Dispatchers;
using Contracts.Objects.Commands.Bash;
using Contracts.Objects.Dtos;
using Microsoft.AspNetCore.Mvc;
using Presentation.Attributes;

namespace Presentation.Api.Controllers
{
    [ApiController]
    [Route("/commands")]
    public class CommandsController(ICommandDispatcher dispatcher)
    {
        [HttpPost]
        [ValidateModelState]
        public async Task<string> Process([FromBody] BashCommandDto body)
        {
            return await dispatcher.ExecuteAsync(new BashExecuteCommand(body));
        }
    }
}
