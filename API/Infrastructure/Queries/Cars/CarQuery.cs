using Contracts.Interfaces.Infrastructure.Context;
using Contracts.Interfaces.Infrastructure.Queries;
using Contracts.Objects.Dtos.Car;
using Contracts.Objects.Dtos.Requests;
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

            List<CarSimpleDto> result = [];

            await using (var reader = (MySqlDataReader)await db.ExecuteReaderAsync(query.ToString(), [.. parameters]))
            {
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
            }

            return result;
        }

        public async Task<CarInfoDto?> GetByIdAsync(int id)
        {
            var query = @$"
                SELECT {string.Join(", ", CarSelects.Info)} FROM {CarSchema.TableName} 
                WHERE {CarSchema.Id} = @id;
            ";

            MySqlParameter[] parameters = [new("id", id)];

            Activity.Current?.SetSqlTag(DbOperation.SELECT, parameters.Length);

            CarInfoDto? result = null;

            await using (var reader = (MySqlDataReader)await db.ExecuteReaderAsync(query, parameters))
            {
                while (await reader.ReadAsync())
                {
                    result = new(
                        reader.GetInt32(CarSchema.Id),
                        reader.GetString(CarSchema.Name),
                        reader.GetString(CarSchema.Plate),
                        reader.GetString(CarSchema.Status)
                    );
                }
            }

            return result;
        }
    }
}
