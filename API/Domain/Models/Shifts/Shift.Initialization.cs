using Domain.Models.Base;
using Domain.ValueObjects.Shifts;

namespace Domain.Models.Shifts
{
    public partial class Shift : DomainModelBase
    {
        private Shift(ShiftId id, ShiftType type, ShiftStatus status, DateTime createdAt, DateTime updatedAt, DateTime? closedAt)
        {
            Id = id;
            Type = type;
            Status = status;
            CreatedAt = createdAt;
            UpdatedAt = updatedAt;
            ClosedAt = closedAt;

            _initialized = true;
        }

        private Shift(ShiftType type)
        {
            Type = type;
            Status = ShiftStatus.Opened;

            CreatedAt = DateTime.Now;
            UpdatedAt = CreatedAt;

            _initialized = true;
        }

        public static Shift Create(ShiftType type) 
            => new(type);

        public static Shift Restore(ShiftId id, ShiftType type, ShiftStatus status, DateTime createdAt, DateTime updatedAt, DateTime? closedAt) 
            => new(id, type, status, createdAt, updatedAt, closedAt);
    }
}
