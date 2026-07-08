using Contracts.Interfaces.Infrastructure.Context;
using Contracts.Interfaces.Infrastructure.Repositories;
using Domain.Models.Parks;
using Domain.ValueObjects.Parks;
using Infrastructure.Internal.Extensions;
using Infrastructure.Internal.Helpers;
using Infrastructure.Schema.Parks;
using MySql.Data.MySqlClient;
using System.Data;

namespace Infrastructure.Repositories.Parks
{
    public class ParkRepository(IDbContext db) : IParkRepository
    {
        public async Task<ParkId> AddAsync(Park park)
        {
            var query = SqlQuery.Insert(ParkSchema.TableName, ParkSelects.Insertation)
                + SqlQuery.SelectLastInsertId;

            MySqlParameter[] parameters = [ParkSchema.Name.ToMysqlParameter(park.Name.Value)];

            await using var command = await db.CreateCommandAsync(query, parameters);
            var idObj = await command.ExecuteScalarAsync();

            return ParkId.FromValue(Convert.ToInt32(idObj));
        }

        public async Task<bool> DeleteAsync(ParkId id)
        {
            var query = SqlQuery.Delete(ParkSchema.TableName, [ParkSchema.Id]);

            MySqlParameter[] parameters = [ParkSchema.Id.ToMysqlParameter(id.Value)];

            await using var command = await db.CreateCommandAsync(query, parameters);
            var affected = await command.ExecuteNonQueryAsync();

            return affected > 0;
        }

        public async Task<Park?> GetAsync(ParkId id)
        {
            var query = SqlQuery.Select(ParkSchema.TableName, ParkSelects.Full, [ParkSchema.Id]);

            MySqlParameter[] parameters = [ParkSchema.Id.ToMysqlParameter(id.Value)];

            await using var command = await db.CreateCommandAsync(query, parameters);
            await using var reader = await command.ExecuteReaderAsync();

            if (await reader.ReadAsync())
                return Park.Restore(
                    id,
                    ParkName.FromValue(reader.GetString(ParkSchema.Name))
                );

            return null;
        }

        public async Task<ParkId?> GetIdByNameAsync(ParkName name)
        {
            var query = SqlQuery.Select(ParkSchema.TableName, [ParkSchema.Id], [ParkSchema.Name]);

            MySqlParameter[] parameters = [ParkSchema.Id.ToMysqlParameter(name.Value)];

            await using var command = await db.CreateCommandAsync(query, parameters);
            var idObj = await command.ExecuteScalarAsync();

            return idObj is null ? null : ParkId.FromValue(Convert.ToInt32(idObj));
        }

        public async Task UpdateAsync(Park park)
        {
            var query = SqlQuery.Update(ParkSchema.TableName, ParkSelects.Full, [ParkSchema.Id]);

            MySqlParameter[] parameters = [
                ParkSchema.Name.ToMysqlParameter(park.Name),
                ParkSchema.Id.ToMysqlParameter(park.Id)
            ];

            await using var command = await db.CreateCommandAsync(query, parameters);
            await command.ExecuteNonQueryAsync();
        }
    }
}
