using Contracts.Interfaces.Application.Services;
using Contracts.Objects.Dtos.Requests;
using Contracts.Objects.Dtos.User;
using Microsoft.AspNetCore.Mvc;
using Presentation.Attributes;
using Presentation.Objects.Requests;
using Presentation.Objects.Responses;
using Presentation.Utilities.Extensions;
using Shared.OpenTelemetry.Tracing.Sources;
using System.Diagnostics;

namespace Presentation.Api.Controllers
{
    [ApiController]
    [Route("/users")]
    public class UsersController
    {
        private readonly IUsersService _service;
        private readonly ActivitySource _activitySource = ActivitySourceDictionary.Controllers.Users;

        public UsersController(IUsersService service, ActivitySource activitySource)
        {
            _service = service;
            _activitySource = activitySource;
        }

        [HttpGet]
        [ValidateModelState]
        public async Task<IReadOnlyList<UserSimpleDto>> GetAll([FromQuery] GetUsersListRequest request)
        {
            using var activity = _activitySource.StartActivity(nameof(GetAll));

            return await _service.GetAllAsync(request);
        }

        [HttpGet("{Id}")]
        [ValidateModelState]
        public async Task<UserInfoDto> GetById([FromRoute] IdRoute route)
        {
            using var activity = _activitySource.StartActivity(nameof(GetById));

            return await _service.GetById(route.Id);
        }

        [HttpGet("username/{Value}")]
        [ValidateModelState]
        public async Task<UserInfoDto> GetByUsername([FromRoute] StringRoute route)
        {
            using var activity = _activitySource.StartActivity(nameof(GetByUsername));

            return await _service.GetByUsernameAsync(route.Value);
        }

        [HttpPost]
        [ValidateModelState]
        public async Task<IActionResult> Post([FromBody] UserCreateDto body)
        {
            using var activity = _activitySource.StartActivity(nameof(Post));

            var result = await _service.CreateAsync(body);

            return ResponseEnvelope.FromData(result).ToCreatedObjectResult();
        }

        [HttpPut("{Id}/info")]
        [ValidateModelState]
        public async Task PutInfoById([FromRoute] IdRoute route, [FromBody] UserInfoUpdateDto body)
        {
            using var activity = _activitySource.StartActivity(nameof(PutInfoById));

            await _service.UpdateInfoByIdAsync(route.Id, body);
        }

        [HttpPut("{Id}/password")]
        [ValidateModelState]
        public async Task PutPasswordById([FromRoute] IdRoute route, [FromBody] UserPasswordUpdateDto body)
        {
            using var activity = _activitySource.StartActivity(nameof(PutPasswordById));

            await _service.UpdatePasswordByIdAsync(route.Id, body);
        }

        [HttpPut("{Id}/role")]
        [ValidateModelState]
        public async Task PutRoleById([FromRoute] IdRoute route, [FromBody] UserRoleUpdateDto body)
        {
            using var activity = _activitySource.StartActivity(nameof(PutRoleById));

            await _service.UpdateRoleByIdAsync(route.Id, body);
        }

        [HttpDelete("{Id}")]
        [ValidateModelState]
        public async Task DeleteById([FromRoute] IdRoute route)
        {
            using var activity = _activitySource.StartActivity(nameof(DeleteById));

            await _service.DeleteByIdAsync(route.Id);
        }
    }
}
