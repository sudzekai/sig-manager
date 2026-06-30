using Contracts.Interfaces.Infrastructure.Repositories;
using Contracts.Objects.Dtos.Requests;
using Domain.Models;
using Infrastructure.Schema.Car;
using MySql.Data.MySqlClient;
using System.Xml.Linq;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Infrastructure.Repositories.Cars
{
    public class CarRepository : ICarRepository
    {
        public Task<int> CreateAsync(Car car)
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

            throw new NotImplementedException();
        }

        public Task DeleteByIdAsync(int id)
        {
            var query = @$"
                DELETE FROM {CarSchema.TableName} 
                WHERE {CarSchema.Id} = @id;
            ";

            MySqlParameter[] parameters = [new("id", id)];

            throw new NotImplementedException();
        }

        public Task<IReadOnlyList<User>> GetAllAsync(GetCarsListRequest request)
        {
            throw new NotImplementedException();
        }

        public Task<User?> GetFullById(int id)
        {
            var query = @$"
                SELECT {string.Join(", ", CarSelects.Full)} FROM {CarSchema.TableName} 
                WHERE {CarSchema.Id} = @id;
            ";

            MySqlParameter[] parameters = [new("id", id)];

            throw new NotImplementedException();
        }

        public Task<User?> GetInfoById(int id)
        {
            var query = @$"
                SELECT {string.Join(", ", CarSelects.Info)} FROM {CarSchema.TableName} 
                WHERE {CarSchema.Id} = @id;
            ";

            MySqlParameter[] parameters = [new("id", id)];

            throw new NotImplementedException();
        }

        public Task<User?> GetInfoByNameAsync(string name)
        {
            var query = @$"
                SELECT {string.Join(", ", CarSelects.Info)} FROM {CarSchema.TableName} 
                WHERE {CarSchema.Name} = @name;
            ";

            MySqlParameter[] parameters = [new("name", name)];

            throw new NotImplementedException();
        }

        public Task<User?> GetInfoByNumberAsync(int number)
        {
            var query = @$"
                SELECT {string.Join(", ", CarSelects.Info)} FROM {CarSchema.TableName} 
                WHERE {CarSchema.Number} = @number;
            ";

            MySqlParameter[] parameters = [new("number", number)];

            throw new NotImplementedException();
        }

        public Task UpdateAsync(Car car)
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

            throw new NotImplementedException();
        }
    }
}
