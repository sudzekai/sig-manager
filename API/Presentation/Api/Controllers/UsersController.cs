using Contracts.Interfaces.Application.Dispatchers;
using Contracts.Objects.Commands.Users.Get;
using Contracts.Objects.Commands.Users.Update;
using Contracts.Objects.Commands.Users.Write;
using Contracts.Objects.Dtos.Requests;
using Contracts.Objects.Dtos.User;
using Microsoft.AspNetCore.Mvc;
using Presentation.Attributes;
using Presentation.Objects.Requests;
using Presentation.Objects.Responses;
using Presentation.Utilities.Extensions;

namespace Presentation.Api.Controllers
{
    [ApiController]
    [Route("/users")]
    public class UsersController(ICommandDispatcher dispatcher)
    {
        [HttpGet]
        [ValidateModelState]
        public async Task<IReadOnlyList<UserSimpleDto>> GetAll([FromQuery] GetUsersListRequest query)
        {
            return await dispatcher.DispatchAsync(new UserGetAllCommand(query));
        }

        [HttpGet("{id}")]
        [ValidateModelState]
        public async Task<UserInfoDto> GetById([FromRoute] IdRoute route)
        {
            return await dispatcher.DispatchAsync(new UserGetCommand(route.Id));
        }

        [HttpPost]
        [ValidateModelState]
        public async Task<IActionResult> Post([FromBody] UserCreateDto body)
        {
            var result = await dispatcher.DispatchAsync(new UserCreateCommand(body));

            return ResponseEnvelope.FromData(result).ToCreatedObjectResult();
        }

        [HttpPut("{id}/info")]
        [ValidateModelState]
        public async Task PutInfoById([FromRoute] IdRoute route, [FromBody] UserInfoUpdateDto body)
        {
            await dispatcher.DispatchAsync(new UserInfoUpdateCommand(route.Id, body));
        }

        [HttpPut("{id}/password")]
        [ValidateModelState]
        public async Task PutPasswordById([FromRoute] IdRoute route, [FromBody] UserPasswordUpdateDto body)
        {
            await dispatcher.DispatchAsync(new UserPasswordUpdateCommand(route.Id, body));
        }

        [HttpPut("{id}/role")]
        [ValidateModelState]
        public async Task PutRoleById([FromRoute] IdRoute route, [FromBody] UserRoleUpdateDto body)
        {
            await dispatcher.DispatchAsync(new UserRoleUpdateCommand(route.Id, body));
        }

        [HttpDelete("{id}")]
        [ValidateModelState]
        public async Task DeleteById([FromRoute] IdRoute route)
        {
            await dispatcher.DispatchAsync(new UserDeleteCommand(route.Id));
        }
    }
}
