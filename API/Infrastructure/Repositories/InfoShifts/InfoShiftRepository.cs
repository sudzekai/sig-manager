using Contracts.Interfaces.Infrastructure.Context;
using Contracts.Interfaces.Infrastructure.Repositories;
using Domain.Models.InfoShifts;
using Domain.ValueObjects.Shifts;
using Domain.ValueObjects.Shifts.Info;
using Infrastructure.Internal.Extensions;
using Infrastructure.Internal.Helpers;
using Infrastructure.Schema.InfoShift;
using MySql.Data.MySqlClient;

namespace Infrastructure.Repositories.InfoShifts
{
    public class InfoShiftRepository(IDbContext db) : IInfoShiftRepository
    {
        public async Task<ShiftId> AddAsync(InfoShift infoShift)
        {
            var query = SqlQuery.Insert(InfoShiftSchema.TableName, InfoShiftSelects.Insertation)
                + SqlQuery.SelectLastInsertId;

            MySqlParameter[] parameters = [
                InfoShiftSchema.ShiftId.ToMysqlParameter(infoShift.ShiftId.Value),
                InfoShiftSchema.Cash.ToMysqlParameter(infoShift.Cash?.Value),
                InfoShiftSchema.Cashless.ToMysqlParameter(infoShift.Cashless?.Value),
                InfoShiftSchema.ReceiptPhotoFileName.ToMysqlParameter(infoShift.ReceiptPhotoFileName?.Value)
            ];

            await using var command = await db.CreateCommandAsync(query, parameters);
            var idObj = await command.ExecuteScalarAsync();

            return infoShift.ShiftId;
        }

        public async Task<bool> DeleteAsync(ShiftId id)
        {
            var query = SqlQuery.Delete(InfoShiftSchema.TableName, [InfoShiftSchema.ShiftId]);

            MySqlParameter[] parameters = [
                InfoShiftSchema.ShiftId.ToMysqlParameter(id.Value)
            ];

            await using var command = await db.CreateCommandAsync(query, parameters);
            var affected = await command.ExecuteNonQueryAsync();

            return affected > 0;
        }

        public async Task<InfoShift?> GetAsync(ShiftId id)
        {
            var query = SqlQuery.Select(InfoShiftSchema.TableName, InfoShiftSelects.Full, [InfoShiftSchema.ShiftId]);

            MySqlParameter[] parameters = [InfoShiftSchema.ShiftId.ToMysqlParameter(id.Value)];

            await using var command = await db.CreateCommandAsync(query, parameters);
            await using var reader = await command.ExecuteReaderAsync();

            if (await reader.ReadAsync())
            {
                var cash = reader.GetNullableDecimal(InfoShiftSchema.Cash);
                var cashless = reader.GetNullableDecimal(InfoShiftSchema.Cashless);
                var fileName = reader.GetNullableString(InfoShiftSchema.Cashless);
                return InfoShift.Restore(
                    id,
                    cash is null ? null : ShiftCash.FromValue(cash.Value),
                    cashless is null ? null : ShiftCashless.FromValue(cashless.Value),
                    fileName is null ? null : ShiftReceiptPhotoFileName.FromValue(fileName)
                );
            }

            return null;
        }

        public async Task UpdateAsync(InfoShift infoShift)
        {
            var query = SqlQuery.Update(InfoShiftSchema.TableName, InfoShiftSelects.Full, [InfoShiftSchema.ShiftId]);

            MySqlParameter[] parameters = [
                InfoShiftSchema.Cash.ToMysqlParameter(infoShift.Cash?.Value),
                InfoShiftSchema.Cashless.ToMysqlParameter(infoShift.Cashless?.Value),
                InfoShiftSchema.ReceiptPhotoFileName.ToMysqlParameter(infoShift.ReceiptPhotoFileName?.Value),
                InfoShiftSchema.ShiftId.ToMysqlParameter(infoShift.ShiftId.Value)
            ];

            await using var command = await db.CreateCommandAsync(query, parameters);
            await command.ExecuteNonQueryAsync();
        }
    }
}
