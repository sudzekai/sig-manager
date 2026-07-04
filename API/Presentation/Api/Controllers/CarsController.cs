using Contracts.Interfaces.Application.Services;
using Contracts.Objects.Dtos.Car;
using Contracts.Objects.Dtos.Requests;
using Microsoft.AspNetCore.Mvc;
using Presentation.Attributes;
using Presentation.Objects.Requests;
using Presentation.Objects.Responses;
using Presentation.Utilities.Extensions;
using Shared.Extensions;
using Shared.OpenTelemetry;

namespace Presentation.Api.Controllers
{
    [ApiController]
    [Route("/cars")]
    public class CarsController(ICarsService service)
    {
        [HttpGet]
        [ValidateModelState]
        public async Task<IReadOnlyList<CarSimpleDto>> GetAll([FromQuery] GetCarsListRequest query)
        {
            using var activity = Telemetry.Controller.StartRichActivity();

            return await service.GetAllAsync(query);
        }

        [HttpGet("{id}")]
        [ValidateModelState]
        public async Task<CarInfoDto> GetById([FromRoute] IdRoute route)
        {
            using var activity = Telemetry.Controller.StartRichActivity();

            return await service.GetByIdAsync(route.Id);
        }

        [HttpPost]
        [ValidateModelState]
        public async Task<IActionResult> Post([FromBody] CarCreateDto body)
        {
            using var activity = Telemetry.Controller.StartRichActivity();

            var result = await service.CreateAsync(body);

            return ResponseEnvelope.FromData(result).ToCreatedObjectResult();
        }

        [HttpPut("{id}")]
        [ValidateModelState]
        public async Task UpdateInfoById([FromRoute] IdRoute route, [FromBody] CarInfoUpdateDto body)
        {
            using var activity = Telemetry.Controller.StartRichActivity();

            await service.UpdateInfoByIdAsync(route.Id, body);
        }

        [HttpPut("{id}/status")]
        [ValidateModelState]
        public async Task UpdateStatusById([FromRoute] IdRoute route, [FromBody] CarStatusUpdateDto body)
        {
            using var activity = Telemetry.Controller.StartRichActivity();

            await service.UpdateStatusByIdAsync(route.Id, body);
        }

        [HttpDelete]
        [ValidateModelState]
        public async Task DeleteById([FromRoute] IdRoute route)
        {
            using var activity = Telemetry.Controller.StartRichActivity();

            await service.DeleteByIdAsync(route.Id);
        }
    }
}
