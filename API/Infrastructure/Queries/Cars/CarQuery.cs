using Contracts.Interfaces.Infrastructure.Context;
using Contracts.Interfaces.Infrastructure.Queries;
using Contracts.Objects.Dtos.Car;
using Contracts.Objects.Dtos.Requests;
using Infrastructure.Internal.Extensions;
using Infrastructure.Schema.Car;
using MySql.Data.MySqlClient;
using Shared.Extensions;
using Shared.Types.Enums;
using System.Diagnostics;
using System.Text;

namespace Infrastructure.Queries.Cars
{
    public class CarQuery(IDbContext db) : ICarQuery
    {
        public async Task<IReadOnlyList<CarSimpleDto>> GetAllAsync(GetCarsListRequest request)
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

            if (!string.IsNullOrWhiteSpace(request.SearchTerm))
            {
                query.Append(@$"
                    AND ({CarSchema.Name} LIKE @searchTerm 
                    OR {CarSchema.Id} LIKE @searchTerm 
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

            List<CarSimpleDto> result = [];

            await using var command = await db.CreateCommandAsync(query.ToString(), [.. parameters]);
            await using var reader = await command.ExecuteReaderAsync();

            var idOrdinal = reader.GetOrdinal(CarSchema.Id);
            var nameOrdinal = reader.GetOrdinal(CarSchema.Name);

            while (await reader.ReadAsync())
            {
                result.Add(
                    new(
                        reader.GetInt32(idOrdinal),
                        reader.GetString(nameOrdinal)
                    )
                );
            }

            return result;
        }

        public async Task<CarInfoDto?> GetByIdAsync(int id)
        {
            var query = @$"
                SELECT {string.Join(", ", CarSelects.Full)} FROM {CarSchema.TableName} 
                WHERE {CarSchema.Id} = @id;
            ";

            MySqlParameter[] parameters = [new("id", id)];

            Activity.Current?.SetSqlTag(DbOperation.SELECT, parameters.Length);

            await using var command = await db.CreateCommandAsync(query.ToString(), [.. parameters]);
            await using var reader = await command.ExecuteReaderAsync();

            if (await reader.ReadAsync())
            {
                return new(
                    reader.GetInt32(CarSchema.Id),
                    reader.GetString(CarSchema.Name),
                    reader.GetString(CarSchema.Plate),
                    reader.GetString(CarSchema.Status)
                );
            }

            return null;
        }
    }
}
