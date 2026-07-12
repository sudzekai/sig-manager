using Contracts.Interfaces.Infrastructure.Context;
using Contracts.Interfaces.Infrastructure.Repositories;
using Domain.Models.Positions;
using Domain.ValueObjects.Positions;
using Infrastructure.Internal.Extensions;
using Infrastructure.Internal.Helpers;
using Infrastructure.Schema.Position;
using MySql.Data.MySqlClient;
using System.Data;

namespace Infrastructure.Repositories.Positions
{
    public class PositionRepository(IDbContext db) : IPositionRepository
    {
        public async Task<PositionId> AddAsync(Position position)
        {
            var query = SqlQuery.Insert(PositionSchema.TableName, PositionSelects.Insertation)
                + SqlQuery.SelectLastInsertId;

            MySqlParameter[] parameters = [
                PositionSchema.Name.ToMysqlParameter(position.Name.Value),
                PositionSchema.PricePerHour.ToMysqlParameter(position.PricePerHour.Value)
            ];

            await using var command = await db.CreateCommandAsync(query, parameters);
            var idObj = await command.ExecuteScalarAsync();

            return PositionId.FromValue(Convert.ToInt32(idObj));
        }

        public async Task<bool> DeleteAsync(PositionId id)
        {
            var query = SqlQuery.Delete(PositionSchema.TableName, [PositionSchema.Id]);

            MySqlParameter[] parameters = [PositionSchema.Id.ToMysqlParameter(id.Value)];

            await using var command = await db.CreateCommandAsync(query, parameters);
            var affected = await command.ExecuteNonQueryAsync();

            return affected > 0;
        }

        public async Task<Position?> GetAsync(PositionId id)
        {
            var query = SqlQuery.Select(PositionSchema.TableName, PositionSelects.Full, [PositionSchema.Id]);

            MySqlParameter[] parameters = [PositionSchema.Id.ToMysqlParameter(id.Value)];

            await using var command = await db.CreateCommandAsync(query, parameters);
            await using var reader = await command.ExecuteReaderAsync();

            if (await reader.ReadAsync())
                return Position.Restore(
                    id,
                    PositionName.FromValue(reader.GetString(PositionSchema.Name)),
                    PositionPricePerHour.FromValue(reader.GetDecimal(PositionSchema.PricePerHour))
                );

            return null;
        }

        public async Task<PositionId?> GetIdByNameAsync(PositionName name)
        {
            var query = SqlQuery.Select(PositionSchema.TableName, [PositionSchema.Id], [PositionSchema.Name]);

            MySqlParameter[] parameters = [PositionSchema.Id.ToMysqlParameter(name.Value)];

            await using var command = await db.CreateCommandAsync(query, parameters);
            var idObj = await command.ExecuteScalarAsync();

            return idObj is null ? null : PositionId.FromValue(Convert.ToInt32(idObj));
        }

        public async Task UpdateAsync(Position position)
        {
            var query = SqlQuery.Update(PositionSchema.TableName, PositionSelects.Full, [PositionSchema.Id]);

            MySqlParameter[] parameters = [
                PositionSchema.Name.ToMysqlParameter(position.Name),
                PositionSchema.PricePerHour.ToMysqlParameter(position.PricePerHour.Value),
                PositionSchema.Id.ToMysqlParameter(position.Id)
            ];

            await using var command = await db.CreateCommandAsync(query, parameters);
            await command.ExecuteNonQueryAsync();
        }
    }
}
