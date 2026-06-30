namespace Contracts.Objects.Dtos.Car
{
    public record CarInfoDto(
        int Id,
        string Name,
        int Number,
        string Plate,
        string Status);
}
