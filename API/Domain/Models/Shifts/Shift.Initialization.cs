namespace Domain.Models.Shifts
{
    public partial class Shift
    {
        private Shift(int id, string type, string status, DateTime createdAt, DateTime updatedAt, DateTime? closedAt)
        {
            _id = id;
            _type = type;
            _status = status;
            CreatedAt = createdAt;
            UpdatedAt = updatedAt;
            ClosedAt = closedAt;

            _initialized = true;
        }

        private Shift(string type)
        {
            Type = type;
            Status = "opened";

            CreatedAt = DateTime.Now;
            UpdatedAt = CreatedAt;

            _initialized = true;
        }

        public static Shift Create(string type) 
            => new(type);

        public static Shift Restore(int id, string type, string status, DateTime createdAt, DateTime updatedAt, DateTime? closedAt) 
            => new(id, type, status, createdAt, updatedAt, closedAt);


        private bool _initialized = false;
    }
}
