namespace Contracts.Objects.Dtos.CarShift
{
    public record CarShiftInfoDto(
        int Id,
        string Status,
        DateTime CreatedAt,
        DateTime? ClosedAt,
        decimal? Cash,
        decimal? CashLess,
        decimal? Total,
        decimal? Difference,
        string? ReceiptPhotoFileName,
        int FirstTicket,
        int? LastTicket,
        int? TotalTickets,
        decimal TicketPrice
    );
}
