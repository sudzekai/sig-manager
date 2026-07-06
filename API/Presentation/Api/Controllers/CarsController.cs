using Contracts.Interfaces.Application.Dispatchers;
using Contracts.Objects.Commands.Cars.Get;
using Contracts.Objects.Commands.Cars.Update;
using Contracts.Objects.Commands.Cars.Write;
using Contracts.Objects.Dtos.Car;
using Contracts.Objects.Dtos.Requests;
using Microsoft.AspNetCore.Mvc;
using Presentation.Attributes;
using Presentation.Objects.Requests;
using Presentation.Objects.Responses;
using Presentation.Utilities.Extensions;

namespace Presentation.Api.Controllers
{
    [ApiController]
    [Route("/cars")]
    public class CarsController(ICommandDispatcher dispatcher)
    {
        [HttpGet]
        [ValidateModelState]
        public async Task<IReadOnlyList<CarSimpleDto>> GetAll([FromQuery] GetCarsListRequest query)
        {
            return await dispatcher.DispatchAsync(new CarGetAllCommand(query));
        }

        [HttpGet("{id}")]
        [ValidateModelState]
        public async Task<CarInfoDto> GetById([FromRoute] IdRoute route)
        {
            return await dispatcher.DispatchAsync(new CarGetCommand(route.Id));
        }

        [HttpPost]
        [ValidateModelState]
        public async Task<IActionResult> Post([FromBody] CarCreateDto body)
        {
            var result = await dispatcher.DispatchAsync(new CarCreateCommand(body));

            return ResponseEnvelope.FromData(result).ToCreatedObjectResult();
        }

        [HttpPut("{id}")]
        [ValidateModelState]
        public async Task UpdateInfoById([FromRoute] IdRoute route, [FromBody] CarInfoUpdateDto body)
        {
            await dispatcher.DispatchAsync(new CarInfoUpdateCommand(route.Id, body));
        }

        [HttpPut("{id}/status")]
        [ValidateModelState]
        public async Task UpdateStatusById([FromRoute] IdRoute route, [FromBody] CarStatusUpdateDto body)
        {
            await dispatcher.DispatchAsync(new CarStatusUpdateCommand(route.Id, body));
        }

        [HttpDelete]
        [ValidateModelState]
        public async Task DeleteById([FromRoute] IdRoute route)
        {
            await dispatcher.DispatchAsync(new CarDeleteCommand(route.Id));
        }
    }
}
