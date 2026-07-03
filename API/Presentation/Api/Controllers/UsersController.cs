using Contracts.Interfaces.Application.Services;
using Contracts.Objects.Dtos.Requests;
using Contracts.Objects.Dtos.User;
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
    [Route("/users")]
    public class UsersController(IUsersService service)
    {
        private readonly IUsersService _service = service;

        [HttpGet]
        [ValidateModelState]
        public async Task<IReadOnlyList<UserSimpleDto>> GetAll([FromQuery] GetUsersListRequest request)
        {
            using var activity = Telemetry.Controller.StartRichActivity();

            return await _service.GetAllAsync(request);
        }

        [HttpGet("{id}")]
        [ValidateModelState]
        public async Task<UserInfoDto> GetById([FromRoute(Name = "id")] IdRoute route)
        {
            using var activity = Telemetry.Controller.StartRichActivity();

            return await _service.GetById(route.Id);
        }

        [HttpGet("username/{value}")]
        [ValidateModelState]
        public async Task<UserInfoDto> GetByUsername([FromRoute(Name = "value")] StringRoute route)
        {
            using var activity = Telemetry.Controller.StartRichActivity();

            return await _service.GetByUsernameAsync(route.Value);
        }

        [HttpPost]
        [ValidateModelState]
        public async Task<IActionResult> Post([FromBody] UserCreateDto body)
        {
            using var activity = Telemetry.Controller.StartRichActivity();

            var result = await _service.CreateAsync(body);

            return ResponseEnvelope.FromData(result).ToCreatedObjectResult();
        }

        [HttpPut("{id}/info")]
        [ValidateModelState]
        public async Task PutInfoById([FromRoute(Name = "id")] IdRoute route, [FromBody] UserInfoUpdateDto body)
        {
            using var activity = Telemetry.Controller.StartRichActivity();

            await _service.UpdateInfoByIdAsync(route.Id, body);
        }

        [HttpPut("{id}/password")]
        [ValidateModelState]
        public async Task PutPasswordById([FromRoute(Name = "id")] IdRoute route, [FromBody] UserPasswordUpdateDto body)
        {
            using var activity = Telemetry.Controller.StartRichActivity();

            await _service.UpdatePasswordByIdAsync(route.Id, body);
        }

        [HttpPut("{id}/role")]
        [ValidateModelState]
        public async Task PutRoleById([FromRoute(Name = "id")] IdRoute route, [FromBody] UserRoleUpdateDto body)
        {
            using var activity = Telemetry.Controller.StartRichActivity();

            await _service.UpdateRoleByIdAsync(route.Id, body);
        }

        [HttpDelete("{id}")]
        [ValidateModelState]
        public async Task DeleteById([FromRoute(Name = "id")] IdRoute route)
        {
            using var activity = Telemetry.Controller.StartRichActivity();

            await _service.DeleteByIdAsync(route.Id);
        }
    }
}
