using Contracts.Objects.Dtos.UserPosition;

namespace Contracts.Objects.Dtos.CarShift
{
    public class CarShiftOpenDto
    {
        public UserPositionDto[] Users { get; set; } = [];
        public int FirstTicket { get; set; }
        public decimal TicketPrice { get; set; }
        public int ParkId { get; set; }
    }
}
