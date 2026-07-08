using Contracts.Objects.Dtos.UserPosition;

namespace Contracts.Objects.Dtos.CarShift
{
    public class CarShiftCreateDto
    {
        public UserPositionDto[] UserPositions { get; set; } = [];
        public int FirstTicket { get; set; }
        public string ParkId { get; set; } = string.Empty;
    }
}
