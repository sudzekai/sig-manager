using Domain.Tools;
using System.Diagnostics.CodeAnalysis;

namespace Domain.Models.Users
{
    public partial class User
    {
        private static void ValidateUsername(string value)
        {
            DataValidator.NullOrWhiteSpace(value, nameof(Username));
            DataValidator.MaxLength(value, 25, nameof(Username));
        }
        private static void ValidateFullName(string value)
        {
            DataValidator.NullOrWhiteSpace(value, nameof(FullName));
            DataValidator.MaxLength(value, 255, nameof(FullName));
        }
        private static void ValidateEmail(string value)
        {
            DataValidator.NullOrWhiteSpace(value, nameof(Email));
            DataValidator.MaxLength(value, 255, nameof(Email));
        }
        private static void ValidatePhoneNumber([NotNull] string? value)
        {
            DataValidator.NullOrWhiteSpace(value, nameof(PhoneNumber));
            DataValidator.MaxLength(value, 15, nameof(PhoneNumber));
            DataValidator.MinLength(value, 12, nameof(PhoneNumber));
        }
        
        private static void ValidatePasswordHash(string value)
        {
            DataValidator.NullOrWhiteSpace(value, nameof(PasswordHash));
        }

        private static void ValidateVerificationCode(string value)
        {
            DataValidator.NullOrWhiteSpace(value, nameof(VerificationCode));
            DataValidator.LengthEquals(value, 6, nameof(VerificationCode));
        }

        private static void ValidateRoleId(int roleId)
        {
            DataValidator.Min(roleId, 1, nameof(roleId));
        }
    }
}
