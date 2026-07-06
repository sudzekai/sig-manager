using Domain.Tools;

namespace Domain.Models.Cars
{
    public partial class Car
    {
        private void ValidateName(string name)
        {
            DataValidator.NullOrWhiteSpace(name, nameof(name));
            DataValidator.MaxLength(name, 50, nameof(name));
        }

        private void ValidateId(int id)
        {
            DataValidator.Min(id, 1, nameof(id));
        }

        private void ValidatePlate(string plate)
        {
            DataValidator.NullOrWhiteSpace(plate, nameof(plate));
            DataValidator.MaxLength(plate, 100, nameof(plate));
        }

        private void ValidateStatus(string status)
        {
            DataValidator.NullOrWhiteSpace(status, nameof(status));
            DataValidator.OneOf(status, ["working", "broken"], nameof(status));
        }
    }
}
