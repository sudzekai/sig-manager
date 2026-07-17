using Contracts.Interfaces.Infrastructure.Context;
using Contracts.Interfaces.Infrastructure.Queries;
using Contracts.Objects.Dtos.CarShift;
using Contracts.Objects.Dtos.Requests;
using Contracts.Objects.Dtos.UserPosition;
using Infrastructure.Internal.Extensions;
using Infrastructure.Schema.CarShift;
using Infrastructure.Schema.InfoShift;
using Infrastructure.Schema.Shift;
using Infrastructure.Schema.TicketShift;
using Infrastructure.Schema.User;
using Infrastructure.Schema.UserShift;
using MySql.Data.MySqlClient;
using System.Data;
using System.Text;

namespace Infrastructure.Queries.CarShifts
{
    public class CarShiftQuery(IDbContext db) : ICarShiftQuery
    {
        public async Task<IReadOnlyList<CarShiftSimpleDto>> GetAllAsync(GetCarShiftsListRequest request)
        {
            StringBuilder query = new($"""
                SELECT 
                    shift.{ShiftSchema.Id} as {ShiftSchema.Id},
                    shift.{ShiftSchema.CreatedAt} as {ShiftSchema.CreatedAt},
                    shift.{ShiftSchema.ClosedAt} as {ShiftSchema.ClosedAt},
                    ticketShift.{TicketShiftSchema.FirstTicket} as {TicketShiftSchema.FirstTicket},
                    ticketShift.{TicketShiftSchema.LastTicket} as {TicketShiftSchema.LastTicket}
                FROM {CarShiftSchema.TableName} carShift
                    INNER JOIN {ShiftSchema.TableName} shift
                        ON shift.{ShiftSchema.Id} = carShift.{CarShiftSchema.ShiftId}
                    LEFT JOIN {TicketShiftSchema.TableName} ticketShift
                        ON ticketShift.{TicketShiftSchema.ShiftId} = carShift.{CarShiftSchema.ShiftId}
                WHERE 
                    1=1
                """);

            List<MySqlParameter> parameters = [];

            if (request.CreatedAtStart != default)
            {
                query.Append($"\nAND {ShiftSchema.CreatedAt} >= @createdAtStart");
                parameters.Add(new("createdAtStart", request.CreatedAtStart));
            }

            if (request.CreatedAtEnd != default)
            {
                query.Append($"\nAND {ShiftSchema.CreatedAt} <= @createdAtEnd");
                parameters.Add(new("createdAtEnd", request.CreatedAtEnd));
            }

            if (request.ClosedAtStart != default)
            {
                query.Append($"\nAND {ShiftSchema.ClosedAt} >= @closedAtStart");
                parameters.Add(new("closedAtStart", request.ClosedAtStart));
            }

            if (request.ClosedAtEnd != default)
            {
                query.Append($"\nAND {ShiftSchema.ClosedAt} <= @closedAtEnd");
                parameters.Add(new("closedAtEnd", request.ClosedAtEnd));
            }

            if (!string.IsNullOrWhiteSpace(request.Status))
            {
                var status = string.Empty;

                if (request.Status.Equals("opened", StringComparison.OrdinalIgnoreCase))
                    status = "opened";

                else if (request.Status.Equals("closed", StringComparison.OrdinalIgnoreCase))
                    status = "closed";

                if (status != string.Empty)
                {
                    query.Append($"\nAND {ShiftSchema.Status} = @{ShiftSchema.Status}");

                    parameters.Add(ShiftSchema.Status.ToMysqlParameter(status));
                }
            }

            if (!string.IsNullOrWhiteSpace(request.OrderBy))
            {
                var orderBy = request.OrderBy.ToLower() switch
                {
                    "status" => ShiftSchema.Status,
                    "closedat" => ShiftSchema.ClosedAt,
                    "openedat" => ShiftSchema.CreatedAt,
                    _ => ShiftSchema.Id
                };

                var direction = request.OrderDirection.ToLower() switch
                {
                    "asc" => "ASC",
                    _ => "DESC"
                };

                query.Append($"\nORDER BY {orderBy} {direction}");
            }
            else
            {
                query.Append($"\nORDER BY {ShiftSchema.Id} DESC");
            }

            query.Append($"\nLIMIT {request.Limit} OFFSET {request.Offset}");

            await using var command = await db.CreateCommandAsync(query.ToString(), [.. parameters]);
            await using var reader = await command.ExecuteReaderAsync();

            List<CarShiftSimpleDto> result = [];

            var idOrdinal = reader.GetOrdinal(ShiftSchema.Id);
            var createdAtOrdinal = reader.GetOrdinal(ShiftSchema.CreatedAt);
            var closedAtOrdinal = reader.GetOrdinal(ShiftSchema.ClosedAt);
            var firstTicketOrdinal = reader.GetOrdinal(TicketShiftSchema.FirstTicket);
            var lastTicketOrdinal = reader.GetOrdinal(TicketShiftSchema.LastTicket);

            while (await reader.ReadAsync())
            {
                result.Add(new(
                    reader.GetInt32(idOrdinal),
                    reader.GetDateTime(createdAtOrdinal),
                    reader.GetNullableDateTime(closedAtOrdinal),
                    reader.GetInt32(firstTicketOrdinal),
                    reader.GetNullableInt32(lastTicketOrdinal)
                ));
            }

            return result;
        }

        public async Task<CarShiftInfoDto?> GetByIdAsync(int id)
        {
            var query = $"""
                SELECT 
                	shift.{ShiftSchema.Id} as {ShiftSchema.Id},
                    carShift.{CarShiftSchema.ParkId} as {CarShiftSchema.ParkId},
                    shift.{ShiftSchema.Status} as {ShiftSchema.Status},
                    shift.{ShiftSchema.CreatedAt} as {ShiftSchema.CreatedAt},
                    shift.{ShiftSchema.ClosedAt} as {ShiftSchema.ClosedAt}, -- nullable
                    infoShift.{InfoShiftSchema.Cash} as {InfoShiftSchema.Cash}, -- nullable
                    infoShift.{InfoShiftSchema.Cashless} as {InfoShiftSchema.Cashless}, -- nullable
                    infoShift.{InfoShiftSchema.ReceiptPhotoFileName} as {InfoShiftSchema.ReceiptPhotoFileName}, -- nullable
                    ticketShift.{TicketShiftSchema.FirstTicket} as {TicketShiftSchema.FirstTicket},
                    ticketShift.{TicketShiftSchema.LastTicket} as {TicketShiftSchema.LastTicket}, -- nullable
                    ticketShift.{TicketShiftSchema.TicketPrice} as {TicketShiftSchema.TicketPrice}
                FROM {CarShiftSchema.TableName} carShift
                	INNER JOIN {ShiftSchema.TableName} shift
                		ON shift.{ShiftSchema.Id} = carShift.{CarShiftSchema.ShiftId}	
                	LEFT JOIN {InfoShiftSchema.TableName} infoShift
                		ON infoShift.{InfoShiftSchema.ShiftId} = shift.{ShiftSchema.Id}
                	LEFT JOIN {TicketShiftSchema.TableName} ticketShift
                		ON ticketShift.{TicketShiftSchema.ShiftId} = shift.{ShiftSchema.Id}
                WHERE shift.{ShiftSchema.Id} = @{ShiftSchema.Id}
                """;

            MySqlParameter[] parameters = [
                ShiftSchema.Id.ToMysqlParameter(id)
                ];

            CarShiftInfoDto? result = null;


            await using (var command = await db.CreateCommandAsync(query, parameters))
            {
                await using var reader = await command.ExecuteReaderAsync();

                if (await reader.ReadAsync())
                {
                    var closedAt = reader.GetNullableDateTime(ShiftSchema.ClosedAt);

                    var cash = reader.GetNullableDecimal(InfoShiftSchema.Cash);
                    var cashless = reader.GetNullableDecimal(InfoShiftSchema.Cashless);
                    var receiptPhotoFileName = reader.GetNullableString(InfoShiftSchema.ReceiptPhotoFileName);

                    var firstTicket = reader.GetInt32(TicketShiftSchema.FirstTicket);
                    var lastTicket = reader.GetNullableInt32(TicketShiftSchema.LastTicket);
                    var ticketPrice = reader.GetDecimal(TicketShiftSchema.TicketPrice);

                    decimal? total = (cash.HasValue && cashless.HasValue) ? cashless.Value + cash.Value : null;
                    int? totalTickets = lastTicket.HasValue ? lastTicket.Value - firstTicket : null;
                    decimal? difference = (cash.HasValue && totalTickets.HasValue) ? cash - totalTickets * ticketPrice : null;

                    result = new(
                        id,
                        reader.GetInt32(CarShiftSchema.ParkId),
                        reader.GetString(ShiftSchema.Status),
                        reader.GetDateTime(ShiftSchema.CreatedAt),
                        closedAt,
                        cash,
                        cashless,
                        total,
                        difference,
                        receiptPhotoFileName,
                        firstTicket,
                        lastTicket,
                        totalTickets,
                        ticketPrice,
                        []
                    );
                }
                else
                    return null;
            }

            query = $"""
                SELECT {UserShiftSchema.UserId}, {UserShiftSchema.PositionId}
                FROM {UserShiftSchema.TableName}
                WHERE {UserShiftSchema.ShiftId} = @{UserShiftSchema.ShiftId}
                """;

            parameters = [
                UserShiftSchema.ShiftId.ToMysqlParameter(id)
                ];

            List<UserPositionDto> users = [];

            await using (var command = await db.CreateCommandAsync(query, parameters))
            {
                await using var reader = await command.ExecuteReaderAsync();

                var idOrdinal = reader.GetOrdinal(UserShiftSchema.UserId);
                var positionIdOrdinal = reader.GetOrdinal(UserShiftSchema.PositionId);

                while (await reader.ReadAsync())
                {
                    users.Add(
                        new()
                        {
                            Id = reader.GetInt32(idOrdinal),
                            PositionId = reader.GetInt32(positionIdOrdinal)
                        }
                    );
                }
            }

            result.Users = [.. users];

            return result;
        }
    }
}
