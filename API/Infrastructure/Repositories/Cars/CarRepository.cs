using Contracts.Interfaces.Infrastructure.Context;
using Contracts.Interfaces.Infrastructure.Repositories;
using Domain.Models;
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
            var query = @$"
                INSERT INTO {CarSchema.TableName} ({string.Join(", ", CarSelects.Insertation)}) 
                VALUES (@id, @name, @plate, @status, @createdAt, @updatedAt);
                SELECT LAST_INSERT_ID();
            ";

            MySqlParameter[] parameters = [
                new("id", car.Id),
                new("name", car.Name),
                new("plate", car.Plate),
                new("status", car.Status),
                new("createdAt", car.CreatedAt),
                new("updatedAt", car.UpdatedAt)
            ];

            Activity.Current?.SetSqlTag(DbOperation.INSERT, parameters.Length);

            var idObj = await db.ExecuteScalarAsync(query, parameters);

            var id = Convert.ToInt32(idObj);

            return id;
        }

        public async Task DeleteAsync(int id)
        {
            var query = @$"
                DELETE FROM {CarSchema.TableName} 
                WHERE {CarSchema.Id} = @id;
            ";

            MySqlParameter[] parameters = [new("id", id)];

            Activity.Current?.SetSqlTag(DbOperation.DELETE, parameters.Length);

            await db.ExecuteNonQueryAsync(query, parameters);
        }

        public async Task<Car?> GetAsync(int id)
        {
            var query = @$"
                SELECT {string.Join(", ", CarSelects.Full)} FROM {CarSchema.TableName} 
                WHERE {CarSchema.Id} = @id;
            ";

            MySqlParameter[] parameters = [new("id", id)];

            Activity.Current?.SetSqlTag(DbOperation.SELECT, parameters.Length);

            Car? result = null;

            await using (var reader = (MySqlDataReader)await db.ExecuteReaderAsync(query, parameters))
            {
                while (await reader.ReadAsync())
                {
                    result = Car.Restore(
                        reader.GetInt32(CarSchema.Id),
                        reader.GetString(CarSchema.Name),
                        reader.GetString(CarSchema.Plate),
                        reader.GetString(CarSchema.Status),
                        reader.GetDateTime(CarSchema.CreatedAt),
                        reader.GetDateTime(CarSchema.UpdatedAt)
                    );
                }
            }

            return result;
        }

        public async Task<int?> GetIdByNameAsync(string name)
        {
            var query = $"""
                SELECT {CarSchema.Id} 
                FROM {CarSchema.TableName}
                WHERE {CarSchema.Name} = @name;
                """;

            MySqlParameter[] parameters = [new("name", name)];
            
            Activity.Current?.SetSqlTag(DbOperation.SELECT, parameters.Length);

            var idObj = await db.ExecuteScalarAsync(query, parameters);

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
                    {CarSchema.Status} = @status,
                    {CarSchema.UpdatedAt} = @updatedAt
                WHERE
                    {CarSchema.Id} = @id
            ";

            MySqlParameter[] parameters = [
                new("id", car.Id),
                new("name", car.Name),
                new("plate", car.Plate),
                new("status", car.Status),
                new("createdAt", car.CreatedAt),
                new("updatedAt", car.UpdatedAt),
                new("id", car.Id)
            ];

            Activity.Current?.SetSqlTag(DbOperation.UPDATE, parameters.Length);

            await db.ExecuteNonQueryAsync(query, parameters);
        }
    }
}
