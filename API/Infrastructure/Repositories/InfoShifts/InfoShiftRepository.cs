using Contracts.Interfaces.Infrastructure.Context;
using Contracts.Interfaces.Infrastructure.Repositories;
using Domain.Models.InfoShifts;
using Infrastructure.Internal.Extensions;
using Infrastructure.Internal.Helpers;
using Infrastructure.Schema.InfoShift;
using MySql.Data.MySqlClient;

namespace Infrastructure.Repositories.InfoShifts
{
    public class InfoShiftRepository(IDbContext db) : IInfoShiftRepository
    {
        public async Task<int> AddAsync(InfoShift infoshift)
        {
            var query = SqlQuery.Insert(InfoShiftSchema.TableName, InfoShiftSelects.Insertation)
                + "SELECT LAST_INSERT_ID();";

            MySqlParameter[] parameters = [
                InfoShiftSchema.ShiftId.ToMysqlParameter(infoshift.ShiftId),
                InfoShiftSchema.Cash.ToMysqlParameter(infoshift.Cash),
                InfoShiftSchema.Cashless.ToMysqlParameter(infoshift.CashLess),
                InfoShiftSchema.ReceiptPhotoFileName.ToMysqlParameter(infoshift.ReceiptPhotoFileName),
            ];

            await using var command = await db.CreateCommandAsync(query.ToString(), [.. parameters]);
            var idObj = await command.ExecuteScalarAsync();

            return Convert.ToInt32(idObj);
        }

        public async Task DeleteAsync(int id)
        {
            var query = SqlQuery.Delete(InfoShiftSchema.TableName, [InfoShiftSchema.ShiftId]);

            MySqlParameter[] parameters = [InfoShiftSchema.ShiftId.ToMysqlParameter(id)];

            await using var command = await db.CreateCommandAsync(query.ToString(), [.. parameters]);
            await command.ExecuteNonQueryAsync();
        }

        public async Task<InfoShift?> GetAsync(int id)
        {
            var query = SqlQuery.Select(InfoShiftSchema.TableName, InfoShiftSelects.Full, [InfoShiftSchema.ShiftId]);

            MySqlParameter[] parameters = [InfoShiftSchema.ShiftId.ToMysqlParameter(id)];

            InfoShift? result = null;

            await using var command = await db.CreateCommandAsync(query.ToString(), [.. parameters]);
            await using var reader = await command.ExecuteReaderAsync();
            
            if (await reader.ReadAsync())
            {
                result = InfoShift.Restore(
                    id,
                    reader.GetNullableDecimal(InfoShiftSchema.Cash),
                    reader.GetNullableDecimal(InfoShiftSchema.Cashless),
                    reader.GetNullableString(InfoShiftSchema.ReceiptPhotoFileName)
                );
            }

            return result;
        }

        public async Task UpdateAsync(InfoShift infoshift)
        {
            var query = SqlQuery.Update(InfoShiftSchema.TableName, InfoShiftSelects.Full, [InfoShiftSchema.ShiftId]);

            MySqlParameter[] parameters = [
                InfoShiftSchema.Cash.ToMysqlParameter(infoshift.Cash),
                InfoShiftSchema.Cashless.ToMysqlParameter(infoshift.CashLess),
                InfoShiftSchema.ReceiptPhotoFileName.ToMysqlParameter(infoshift.ReceiptPhotoFileName)
            ];

            await using var command = await db.CreateCommandAsync(query.ToString(), [.. parameters]);
            await command.ExecuteNonQueryAsync();
        }
    }
}
