namespace Domain.Models.TicketShifts
{
    public partial class TicketShift
    {
        public int ShiftId { get; private set; }
        
        public int FirstTicket { get; private set; }
        
        public int? LastTicket { get; private set; }

        public decimal TicketPrice { get; private set; }
    }
}
