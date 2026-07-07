using Domain.Tools;
using System.Diagnostics.CodeAnalysis;

namespace Domain.Models.TicketShifts
{
    public partial class TicketShift
    {
        private void ValidateShiftId(int shiftId)
        {
            DataValidator.Min(shiftId, 1, nameof(shiftId));
        }

        private void ValidateFirstTicket(int firstTicket)
        {
            DataValidator.Min(firstTicket, 1, nameof(firstTicket));
        }

        private void ValidateLastTicket([NotNull] int? lastTicket)
        {
            DataValidator.Null(lastTicket, nameof(lastTicket));
            DataValidator.Min(lastTicket.Value, 1, nameof(lastTicket));
        }

        private void ValidateTicketPrice(decimal ticketPrice)
        {
            DataValidator.Min(ticketPrice, 200, nameof(ticketPrice));
        }
    }
}
