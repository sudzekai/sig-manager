namespace Contracts.Objects.Dtos.User
{
    public record UserInfoDto
        (int Id,
        string Username,
        string FullName,
        string Email,
        string PhoneNumber)
    { }
}
