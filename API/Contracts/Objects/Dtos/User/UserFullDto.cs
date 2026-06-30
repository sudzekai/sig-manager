namespace Contracts.Objects.Dtos.User
{
    public record UserFullDto
        (int Id,
        string Username,
        string FullName,
        string Email,
        string PhoneNumber,
        string PasswordHash,
        string VerificationCode,
        DateTime CreatedAt,
        DateTime UpdatedAt,
        int RoleId)
    { }
}
