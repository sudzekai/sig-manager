namespace Domain.Models.Shifts
{
    public partial class Shift
    {
        public int Id { get; private set; }
        
        public string Type { get; private set; }
        
        public string Status { get; private set; }
        
        public DateTime CreatedAt { get; private set; }

        public DateTime UpdatedAt { get; private set; }

        public DateTime? ClosedAt { get; private set; }
    }
}
