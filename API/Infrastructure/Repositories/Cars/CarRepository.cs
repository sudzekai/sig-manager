using Contracts.Interfaces.Infrastructure.Context;
using Contracts.Interfaces.Infrastructure.Repositories;
using Domain.Models.Cars;
using Infrastructure.Internal.Extensions;
using Infrastructure.Internal.Helpers;
using Infrastructure.Schema.Car;
using MySql.Data.MySqlClient;
using Shared.Extensions;
using Shared.Types.Enums;
using System.Diagnostics;

namespace Infrastructure.Repositories.Cars
{
    public class CarRepository(IDbContext db) : ICarRepository
    {
        public async Task<int> AddAsync(Car car)
        {
            var query = SqlQuery.Insert(CarSchema.TableName, CarSelects.Insertation);

            MySqlParameter[] parameters = [
                CarSchema.Id.ToMysqlParameter(car.Id),
                CarSchema.Name.ToMysqlParameter(car.Name),
                CarSchema.Plate.ToMysqlParameter(car.Plate),
                CarSchema.Status.ToMysqlParameter(car.Status)
            ];

            Activity.Current?.SetSqlTag(DbOperation.INSERT, parameters.Length);

            await using var command = await db.CreateCommandAsync(query.ToString(), [.. parameters]);
            var idObj = await command.ExecuteScalarAsync();

            return Convert.ToInt32(idObj);
        }

        public async Task DeleteAsync(int id)
        {
            var query = SqlQuery.Delete(CarSchema.TableName, [CarSchema.Id]);

            MySqlParameter[] parameters = [CarSchema.Id.ToMysqlParameter(id)];

            Activity.Current?.SetSqlTag(DbOperation.DELETE, parameters.Length);

            await using var command = await db.CreateCommandAsync(query.ToString(), [.. parameters]);
            await command.ExecuteNonQueryAsync();
        }

        public async Task<Car?> GetAsync(int id)
        {
            var query = SqlQuery.Select(CarSchema.TableName, CarSelects.Full, [CarSchema.Id]);

            MySqlParameter[] parameters = [CarSchema.Id.ToMysqlParameter(id)];

            Activity.Current?.SetSqlTag(DbOperation.SELECT, parameters.Length);

            await using var command = await db.CreateCommandAsync(query.ToString(), [.. parameters]);
            await using var reader = await command.ExecuteReaderAsync();

            if (await reader.ReadAsync())
            {
                return Car.Restore(
                    reader.GetInt32(CarSchema.Id),
                    reader.GetString(CarSchema.Name),
                    reader.GetString(CarSchema.Plate),
                    reader.GetString(CarSchema.Status)
                );
            }

            return null;
        }

        public async Task<int?> GetIdByNameAsync(string name)
        {
            var query = SqlQuery.Select(CarSchema.TableName, [CarSchema.Id], [CarSchema.Name]);

            MySqlParameter[] parameters = [CarSchema.Name.ToMysqlParameter(name)];
            
            Activity.Current?.SetSqlTag(DbOperation.SELECT, parameters.Length);
            
            await using var command = await db.CreateCommandAsync(query.ToString(), [.. parameters]);
            var idObj = await command.ExecuteScalarAsync();

            if (idObj is null) 
                return null;

            return Convert.ToInt32(idObj);
        }

        public async Task UpdateAsync(Car car)
        {
            var query = $@"
                UPDATE {CarSchema.TableName}
                SET 
                    {CarSchema.Id} = @id,
                    {CarSchema.Name} = @name,
                    {CarSchema.Plate} = @plate,
                    {CarSchema.Status} = @status
                WHERE
                    {CarSchema.Id} = @id
            ";

            MySqlParameter[] parameters = [
                new("id", car.Id),
                new("name", car.Name),
                new("plate", car.Plate),
                new("status", car.Status),
            ];

            Activity.Current?.SetSqlTag(DbOperation.UPDATE, parameters.Length);
            
            await using var command = await db.CreateCommandAsync(query.ToString(), [.. parameters]);
            await command.ExecuteNonQueryAsync();
        }
    }
}
