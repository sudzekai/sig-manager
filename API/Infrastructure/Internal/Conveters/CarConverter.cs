using Domain.Models;
using Infrastructure.Internal.Extensions;
using Infrastructure.Schema.Car;
using System.Data;

namespace Infrastructure.Internal.Conveters
{
    internal static class CarConverter
    {
        public static Car? FromReader(IDataReader reader)
        {
            if (!reader.Read())
                return null;

            var car = Car.Restore(
                reader.TryGetInt32(CarSchema.Id),
                reader.TryGetString(CarSchema.Name),
                reader.TryGetInt32(CarSchema.Number),
                reader.TryGetString(CarSchema.Plate),
                reader.TryGetString(CarSchema.Status),
                reader.TryGetDateTime(CarSchema.CreatedAt),
                reader.TryGetDateTime(CarSchema.UpdatedAt)
            );

            return car;
        }

        public static IReadOnlyList<Car> ListFromReader(IDataReader reader)
        {
            reader.TryGetOrdinal(CarSchema.Id, out var idOrdinal);
            reader.TryGetOrdinal(CarSchema.Name, out var nameOrdinal);
            reader.TryGetOrdinal(CarSchema.Number, out var numberOrdinal);
            reader.TryGetOrdinal(CarSchema.Plate, out var plateOrdinal);
            reader.TryGetOrdinal(CarSchema.Status, out var statusOrdinal);
            reader.TryGetOrdinal(CarSchema.CreatedAt, out var createdAtOrdinal);
            reader.TryGetOrdinal(CarSchema.UpdatedAt, out var updatedAtOrdinal);

            List<Car> cars = [];

            while (reader.Read())
            {
                cars.Add(Car.Restore(
                    reader.TryGetInt32(idOrdinal),
                    reader.TryGetString(nameOrdinal),
                    reader.TryGetInt32(numberOrdinal),
                    reader.TryGetString(plateOrdinal),
                    reader.TryGetString(statusOrdinal),
                    reader.TryGetDateTime(createdAtOrdinal),
                    reader.TryGetDateTime(updatedAtOrdinal)
                ));
            }

            return cars;
        }
    }
}
