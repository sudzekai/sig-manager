namespace Contracts.Objects.Dtos.Models.Requests
{
    public record UserCreateRequest
        (string Username,
        string FullName,
        string Email,
        string PhoneNumber,
        string Password)
    { }
}
