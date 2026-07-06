namespace Contracts.Objects.Dtos.CarShift
{
    public record CarShiftSimpleDto(
        int Id,
        DateTime CreatedAt,
        DateTime? ClosedAt,
        int FirstTicket,
        int? LastTicket
    );
}
