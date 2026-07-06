using Contracts.Interfaces.Application.Dispatchers;
using Contracts.Objects.Commands.Users.Update;
using Contracts.Objects.Commands.Users.Write;
using Contracts.Objects.Dtos.Requests;
using Contracts.Objects.Dtos.User;
using Contracts.Objects.Queries.Users;
using Microsoft.AspNetCore.Mvc;
using Presentation.Attributes;
using Presentation.Objects.Requests;
using Presentation.Objects.Responses;
using Presentation.Utilities.Extensions;

namespace Presentation.Api.Controllers
{
    [ApiController]
    [Route("/users")]
    public class UsersController(ICommandDispatcher commandDispatcher, IQueryDispatcher queryDispatcher)
    {
        [HttpGet]
        [ValidateModelState]
        public async Task<IReadOnlyList<UserSimpleDto>> GetAll([FromQuery] GetUsersListRequest query)
        {
            return await queryDispatcher.QueryAsync(new UserGetAllQuery(query));
        }

        [HttpGet("{id}")]
        [ValidateModelState]
        public async Task<UserInfoDto> GetById([FromRoute] IdRoute route)
        {
            return await queryDispatcher.QueryAsync(new UserGetQuery(route.Id));
        }

        [HttpPost]
        [ValidateModelState]
        public async Task<IActionResult> Post([FromBody] UserCreateDto body)
        {
            var result = await commandDispatcher.ExecuteAsync(new UserCreateCommand(body));

            return ResponseEnvelope.FromData(result).ToCreatedObjectResult();
        }

        [HttpPut("{id}/info")]
        [ValidateModelState]
        public async Task PutInfoById([FromRoute] IdRoute route, [FromBody] UserInfoUpdateDto body)
        {
            await commandDispatcher.ExecuteAsync(new UserInfoUpdateCommand(route.Id, body));
        }

        [HttpPut("{id}/password")]
        [ValidateModelState]
        public async Task PutPasswordById([FromRoute] IdRoute route, [FromBody] UserPasswordUpdateDto body)
        {
            await commandDispatcher.ExecuteAsync(new UserPasswordUpdateCommand(route.Id, body));
        }

        [HttpPut("{id}/role")]
        [ValidateModelState]
        public async Task PutRoleById([FromRoute] IdRoute route, [FromBody] UserRoleUpdateDto body)
        {
            await commandDispatcher.ExecuteAsync(new UserRoleUpdateCommand(route.Id, body));
        }

        [HttpDelete("{id}")]
        [ValidateModelState]
        public async Task DeleteById([FromRoute] IdRoute route)
        {
            await commandDispatcher.ExecuteAsync(new UserDeleteCommand(route.Id));
        }
    }
}
