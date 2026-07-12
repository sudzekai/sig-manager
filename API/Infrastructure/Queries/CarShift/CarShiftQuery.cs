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
using Infrastructure.Schema.UserShift;
using MySql.Data.MySqlClient;
using Shared.App;
using System.Data;

namespace Infrastructure.Queries.CarShift
{
    public class CarShiftQuery(IDbContext db) : ICarShiftQuery
    {
        public async Task<IReadOnlyList<CarShiftSimpleDto>> GetAllAsync(GetCarShiftsListRequest request)
        {
            throw AppConstants.TODO();
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
                    decimal? difference = (total.HasValue && totalTickets.HasValue) ? total - totalTickets * ticketPrice : null;

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

            result.Users = [..users];

            return result;
        }
    }
}
