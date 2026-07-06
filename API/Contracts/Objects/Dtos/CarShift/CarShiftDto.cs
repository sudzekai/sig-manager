namespace Contracts.Objects.Dtos.CarShift
{
    public record CarShiftDto(
        int Id,
        string Status,
        DateTime CreatedAt,
        DateTime? ClosedAt,
        decimal? Cash,
        decimal? CashLess,
        string? ReceiptPhotoFileName,
        int FirstTicket,
        int? LastTicket,
        decimal TicketPrice
    );
}
