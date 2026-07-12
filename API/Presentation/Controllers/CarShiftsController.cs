using Contracts.Interfaces.Application.Dispatchers;
using Contracts.Objects.Commands.CarShifts;
using Contracts.Objects.Dtos.CarShift;
using Contracts.Objects.Dtos.Requests;
using Contracts.Objects.Queries.CarShifts;
using Microsoft.AspNetCore.Mvc;
using Presentation.Attributes;
using Presentation.Internal.Objects.Requests;
using Presentation.Internal.Objects.Responses;
using Presentation.Internal.Utilities.Extensions;

namespace Presentation.Controllers
{
    [ApiController]
    [Route("/shifts/cars")]
    public class CarShiftsController(ICommandDispatcher commandDispatcher, IQueryDispatcher queryDispatcher)
    {
        [HttpGet]
        [ValidateModelState]
        public async Task<IReadOnlyList<CarShiftSimpleDto>> GetAll([FromQuery] GetCarShiftsListRequest query)
        {
            return await queryDispatcher.QueryAsync(new CarShiftGetAllQuery(query));
        }

        [HttpGet("{id}")]
        [ValidateModelState]
        public async Task<CarShiftInfoDto> GetById([FromRoute] IdRoute route)
        {
            return await queryDispatcher.QueryAsync(new CarShiftGetQuery(route.Id));
        }

        [HttpPost()]
        [ValidateModelState]
        public async Task<IActionResult> Open([FromBody] CarShiftOpenDto body)
        {
            var created = await commandDispatcher.ExecuteAsync(new CarShiftOpenCommand(body));

            return ResponseEnvelope.FromData(created).ToCreatedObjectResult();
        }

        [HttpPut("{id}")]
        [ValidateModelState]
        public async Task<CarShiftInfoDto> Close([FromRoute] IdRoute route, [FromBody] CarShiftCloseDto body)
        {
            return await commandDispatcher.ExecuteAsync(new CarShiftCloseCommand(route.Id, body));
        }

        [HttpDelete("{id}")]
        [ValidateModelState]
        public async Task Delete([FromRoute] IdRoute route)
        {
            await commandDispatcher.ExecuteAsync(new CarShiftDeleteCommand(route.Id));
        }
    }
}
