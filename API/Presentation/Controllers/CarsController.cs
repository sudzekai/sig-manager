using Contracts.Interfaces.Application.Dispatchers;
using Contracts.Objects.Commands.Cars.Update;
using Contracts.Objects.Commands.Cars.Write;
using Contracts.Objects.Dtos.Car;
using Contracts.Objects.Dtos.Requests;
using Contracts.Objects.Queries.Cars;
using Microsoft.AspNetCore.Mvc;
using Presentation.Attributes;
using Presentation.Internal.Objects.Requests;
using Presentation.Internal.Objects.Responses;
using Presentation.Internal.Utilities.Extensions;

namespace Presentation.Controllers
{
    [ApiController]
    [Route("/cars")]
    public class CarsController(ICommandDispatcher commandDispatcher, IQueryDispatcher queryDispatcher)
    {
        [HttpGet]
        [ValidateModelState]
        public async Task<IReadOnlyList<CarSimpleDto>> GetAll([FromQuery] GetCarsListRequest query)
        {
            return await queryDispatcher.QueryAsync(new CarGetAllQuery(query));
        }

        [HttpGet("{id}")]
        [ValidateModelState]
        public async Task<CarInfoDto> GetById([FromRoute] IdRoute route)
        {
            return await queryDispatcher.QueryAsync(new CarGetQuery(route.Id));
        }

        [HttpPost]
        [ValidateModelState]
        public async Task<IActionResult> Post([FromBody] CarCreateDto body)
        {
            var result = await commandDispatcher.ExecuteAsync(new CarCreateCommand(body));

            return ResponseEnvelope.FromData(result).ToCreatedObjectResult();
        }

        [HttpPut("{id}")]
        [ValidateModelState]
        public async Task UpdateInfoById([FromRoute] IdRoute route, [FromBody] CarInfoUpdateDto body)
        {
            await commandDispatcher.ExecuteAsync(new CarInfoUpdateCommand(route.Id, body));
        }

        [HttpPut("{id}/status")]
        [ValidateModelState]
        public async Task UpdateStatusById([FromRoute] IdRoute route, [FromBody] CarStatusUpdateDto body)
        {
            await commandDispatcher.ExecuteAsync(new CarStatusUpdateCommand(route.Id, body));
        }

        [HttpDelete]
        [ValidateModelState]
        public async Task DeleteById([FromRoute] IdRoute route)
        {
            await commandDispatcher.ExecuteAsync(new CarDeleteCommand(route.Id));
        }
    }
}
