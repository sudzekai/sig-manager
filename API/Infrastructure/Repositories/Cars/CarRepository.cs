using Contracts.Interfaces.Infrastructure.Context;
using Contracts.Interfaces.Infrastructure.Repositories;
using Domain.Models.Cars;
using Domain.ValueObjects.Cars;
using Infrastructure.Internal.Extensions;
using Infrastructure.Internal.Helpers;
using Infrastructure.Schema.Car;
using MySql.Data.MySqlClient;
using System.Data;

namespace Infrastructure.Repositories.Cars
{
    public class CarRepository(IDbContext db) : ICarRepository
    {
        public async Task<CarId> AddAsync(Car car)
        {
            var query = SqlQuery.Insert(CarSchema.TableName, CarSelects.Insertation)
                + SqlQuery.SelectLastInsertId;

            MySqlParameter[] parameters = [
                CarSchema.Id.ToMysqlParameter(car.Id.Value),
                CarSchema.Name.ToMysqlParameter(car.Name.Value),
                CarSchema.Plate.ToMysqlParameter(car.Plate.Value),
                CarSchema.Status.ToMysqlParameter(car.Status.Value)
            ];

            await using var command = await db.CreateCommandAsync(query, parameters);
            var idObj = await command.ExecuteScalarAsync();

            return CarId.FromValue(Convert.ToInt32(idObj));
        }

        public async Task<bool> DeleteAsync(CarId id)
        {
            var query = SqlQuery.Delete(CarSchema.TableName, [CarSchema.Id]);

            MySqlParameter[] parameters = [CarSchema.Id.ToMysqlParameter(id.Value)];

            await using var command = await db.CreateCommandAsync(query, parameters);
            var affected = await command.ExecuteNonQueryAsync();

            return affected > 0;
        }

        public async Task<Car?> GetAsync(CarId id)
        {
            var query = SqlQuery.Select(CarSchema.TableName, CarSelects.Full, [CarSchema.Id]);

            MySqlParameter[] parameters = [CarSchema.Id.ToMysqlParameter(id.Value)];

            await using var command = await db.CreateCommandAsync(query, parameters);
            await using var reader = await command.ExecuteReaderAsync();

            if (await reader.ReadAsync())
                return Car.Restore(
                    id,
                    CarName.FromValue(reader.GetString(CarSchema.Name)),
                    CarPlate.FromValue(reader.GetString(CarSchema.Plate)),
                    CarStatus.FromValue(reader.GetString(CarSchema.Status))
                );

            return null;
        }

        public async Task<CarId?> GetIdByNameAsync(CarName name)
        {
            var query = SqlQuery.Select(CarSchema.TableName, [CarSchema.Id], [CarSchema.Name]);

            MySqlParameter[] parameters = [CarSchema.Name.ToMysqlParameter(name.Value)];

            await using var command = await db.CreateCommandAsync(query, parameters);
            var idObj = await command.ExecuteScalarAsync();

            return idObj is null ? null : CarId.FromValue(Convert.ToInt32(idObj));
        }

        public async Task UpdateAsync(Car car)
        {
            var query = SqlQuery.Update(CarSchema.TableName, CarSelects.Full, [CarSchema.Id]);

            MySqlParameter[] parameters = [
                CarSchema.Name.ToMysqlParameter(car.Name.Value),
                CarSchema.Plate.ToMysqlParameter(car.Plate.Value),
                CarSchema.Status.ToMysqlParameter(car.Status.Value),
                CarSchema.Status.ToMysqlParameter(car.Id.Value),
            ];

            await using var command = await db.CreateCommandAsync(query, parameters);
            await command.ExecuteNonQueryAsync();
        }
    }
}
