namespace Contracts.Objects.Dtos.Car
{
    public record CarFullDto(
        int Id,
        string Name,
        string Plate,
        string Status,
        DateTime CreatedAt,
        DateTime UpdatedAt
    );
}
