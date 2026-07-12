using Contracts.Interfaces.Application.Commands;
using Contracts.Objects.Dtos.Parks;

namespace Contracts.Objects.Queries.Parks
{
    public record ParkGetQuery(int Id) : ICommand<ParkDto>;
}
