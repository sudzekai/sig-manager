namespace Infrastructure.Schema.TicketShift
{
    internal class TicketShiftSelects
    {
        public static readonly IReadOnlyList<string> Insertation = [
            TicketShiftSchema.ShiftId,
            TicketShiftSchema.FirstTicket,
            TicketShiftSchema.TicketPrice,
        ];

        public static readonly IReadOnlyList<string> Full = [
            TicketShiftSchema.FirstTicket,
            TicketShiftSchema.LastTicket,
            TicketShiftSchema.TicketPrice,
        ];
    }
}
