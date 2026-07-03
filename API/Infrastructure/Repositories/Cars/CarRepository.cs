using Contracts.Interfaces.Infrastructure.Context;
using Contracts.Interfaces.Infrastructure.Repositories;
using Contracts.Objects.Dtos.Requests;
using Domain.Models;
using Infrastructure.Internal.Conveters;
using Infrastructure.Schema.Car;
using MySql.Data.MySqlClient;
using Shared.Extensions;
using Shared.Types.Enums;
using System.Diagnostics;
using System.Text;

namespace Infrastructure.Repositories.Cars
{
    public class CarRepository(IDbContext db) : ICarRepository
    {
        public async Task<int> CreateAsync(Car car)
        {
            var query = @$"
                INSERT INTO {CarSchema.TableName} ({string.Join(", ", CarSelects.Insertation)}) 
                VALUES (@name, @number, @plate, @status, @createdAt, @updatedAt);
                SELECT LAST_INSERT_ID();
            ";

            MySqlParameter[] parameters = [
                new("name", car.Name),
                new("number", car.Number),
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

        public async Task DeleteByIdAsync(int id)
        {
            var query = @$"
                DELETE FROM {CarSchema.TableName} 
                WHERE {CarSchema.Id} = @id;
            ";

            MySqlParameter[] parameters = [new("id", id)];

            Activity.Current?.SetSqlTag(DbOperation.DELETE, parameters.Length);

            await db.ExecuteNonQueryAsync(query, parameters);
        }

        public async Task<IReadOnlyList<Car>> GetAllAsync(GetCarsListRequest request)
        {
            StringBuilder query = new($@"
                SELECT {string.Join(", ", CarSelects.Simple)} 
                FROM {CarSchema.TableName}
                WHERE 1=1
            ");

            List<MySqlParameter> parameters = [];

            if (!string.IsNullOrWhiteSpace(request.Status))
            {
                query.Append($"\nAND {CarSchema.Status} = @status");
                parameters.Add(new("status", request.Status.ToLower()));
            }

            if (request.CreatedAtStart != default)
            {
                query.Append($"\nAND {CarSchema.CreatedAt} > @createdAtStart");
                parameters.Add(new("createdAtStart", request.CreatedAtStart));
            }

            if (request.CreatedAtEnd != default)
            {
                query.Append($"\nAND {CarSchema.CreatedAt} < @createdAtEnd");
                parameters.Add(new("createdAtEnd", request.CreatedAtEnd));
            }

            if (request.UpdatedAtStart != default)
            {
                query.Append($"\nAND {CarSchema.UpdatedAt} > @updatedAtStart");
                parameters.Add(new("updatedAtStart", request.UpdatedAtStart));
            }

            if (request.UpdatedAtEnd != default)
            {
                query.Append($"\nAND {CarSchema.UpdatedAt} < @updatedAtEnd");
                parameters.Add(new("updatedAtEnd", request.UpdatedAtEnd));
            }

            if (!string.IsNullOrWhiteSpace(request.SearchTerm))
            {
                query.Append(@$"
                    AND ({CarSchema.Name} LIKE @searchTerm 
                    OR {CarSchema.Number} LIKE @searchTerm 
                    OR {CarSchema.Plate} LIKE @searchTerm)
                ");

                parameters.Add(new("searchTerm", $"{request.SearchTerm}%"));
            }

            if (!string.IsNullOrWhiteSpace(request.OrderBy))
            {
                var orderBy = request.OrderBy.ToLower() switch
                {
                    "name" => CarSchema.Name,
                    "status" => CarSchema.Status,
                    "number" => CarSchema.Number,
                    "createdate" => CarSchema.CreatedAt,
                    "updatedate" => CarSchema.UpdatedAt,
                    _ => CarSchema.Id
                };

                var orderDirection = request.OrderDirection.Equals("asc", StringComparison.OrdinalIgnoreCase)
                    ? "ASC"
                    : "DESC";

                query.Append($"\nORDER BY {orderBy} {orderDirection}");
            }

            query.Append($"\nLIMIT @limit OFFSET @offset;");

            parameters.Add(new("limit", request.Limit));
            parameters.Add(new("offset", request.Offset));

            Activity.Current?.SetSqlTag(DbOperation.SELECT, parameters.Count);

            using var reader = await db.ExecuteReaderAsync(query.ToString(), [.. parameters]);

            var result = CarConverter.ListFromReader(reader);

            return result;
        }

        public async Task<Car?> GetFullByIdAsync(int id)
        {
            var query = @$"
                SELECT {string.Join(", ", CarSelects.Full)} FROM {CarSchema.TableName} 
                WHERE {CarSchema.Id} = @id;
            ";

            MySqlParameter[] parameters = [new("id", id)];

            Activity.Current?.SetSqlTag(DbOperation.SELECT, parameters.Length);

            using var reader = await db.ExecuteReaderAsync(query, parameters);

            var result = CarConverter.FromReader(reader);

            return result;
        }

        public async Task<Car?> GetInfoByIdAsync(int id)
        {
            var query = @$"
                SELECT {string.Join(", ", CarSelects.Info)} FROM {CarSchema.TableName} 
                WHERE {CarSchema.Id} = @id;
            ";

            MySqlParameter[] parameters = [new("id", id)];

            Activity.Current?.SetSqlTag(DbOperation.SELECT, parameters.Length);

            using var reader = await db.ExecuteReaderAsync(query, parameters);

            var result = CarConverter.FromReader(reader);

            return result;
        }

        public async Task<Car?> GetInfoByNameAsync(string name)
        {
            var query = @$"
                SELECT {string.Join(", ", CarSelects.Info)} FROM {CarSchema.TableName} 
                WHERE {CarSchema.Name} = @name;
            ";

            MySqlParameter[] parameters = [new("name", name)];

            Activity.Current?.SetSqlTag(DbOperation.SELECT, parameters.Length);
            
            using var reader = await db.ExecuteReaderAsync(query, parameters);

            var result = CarConverter.FromReader(reader);

            return result;
        }

        public async Task<Car?> GetInfoByNumberAsync(int number)
        {
            var query = @$"
                SELECT {string.Join(", ", CarSelects.Info)} FROM {CarSchema.TableName} 
                WHERE {CarSchema.Number} = @number;
            ";

            MySqlParameter[] parameters = [new("number", number)];

            Activity.Current?.SetSqlTag(DbOperation.SELECT, parameters.Length);
            
            using var reader = await db.ExecuteReaderAsync(query, parameters);

            var result = CarConverter.FromReader(reader);

            return result;
        }

        public async Task UpdateAsync(Car car)
        {
            var query = $@"
                UPDATE {CarSchema.TableName}
                SET 
                    {CarSchema.Name} = @name,
                    {CarSchema.Number} = @number,
                    {CarSchema.Plate} = @plate,
                    {CarSchema.Status} = @status,
                    {CarSchema.UpdatedAt} = @updatedAt
                WHERE
                    {CarSchema.Id} = @id
            ";

            MySqlParameter[] parameters = [
                new("name", car.Name),
                new("number", car.Number),
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
