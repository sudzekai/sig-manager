using Contracts.Objects.Attributes;
using System.ComponentModel.DataAnnotations;

namespace Contracts.Objects.Dtos.User
{
    public record UserInfoUpdateDto
    {
        public string? Username { get; set; }

        public string? FullName { get; set; }

        [EmailAddress]
        public string? Email { get; set; }

        [PhoneNumber]
        public string? PhoneNumber { get; set; }
    }
}
