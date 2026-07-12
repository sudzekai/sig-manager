using Contracts.Objects.Dtos.UserPosition;

namespace Contracts.Objects.Dtos.CarShift
{
    public class CarShiftInfoDto
    {
        public CarShiftInfoDto() { }

        public CarShiftInfoDto(
            int id,
            int parkId,
            string status,
            DateTime createdAt,
            DateTime? closedAt,
            decimal? cash,
            decimal? cashLess,
            decimal? total,
            decimal? difference,
            string? receiptPhotoFileName,
            int firstTicket,
            int? lastTicket,
            int? totalTickets,
            decimal ticketPrice,
            UserPositionDto[] users)
        {
            Id = id;
            ParkId = parkId;
            Status = status;
            CreatedAt = createdAt;
            ClosedAt = closedAt;
            Cash = cash;
            CashLess = cashLess;
            Total = total;
            Difference = difference;
            ReceiptPhotoFileName = receiptPhotoFileName;
            FirstTicket = firstTicket;
            LastTicket = lastTicket;
            TotalTickets = totalTickets;
            TicketPrice = ticketPrice;
            Users = users;
        }

        public int Id { get; set; } = default;

        public int ParkId { get; set; } = default;

        public string Status { get; set; } = string.Empty;

        public DateTime CreatedAt { get; set; } = default;

        public DateTime? ClosedAt { get; set; }

        public decimal? Cash { get; set; }

        public decimal? CashLess { get; set; }

        public decimal? Total { get; set; }

        public decimal? Difference { get; set; }

        public string? ReceiptPhotoFileName { get; set; }

        public int FirstTicket { get; set; } = default;

        public int? LastTicket { get; set; }

        public int? TotalTickets { get; set; }

        public decimal TicketPrice { get; set; } = default;

        public UserPositionDto[] Users { get; set; } = [];
    }
}
