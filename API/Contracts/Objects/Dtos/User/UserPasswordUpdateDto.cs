using Contracts.Objects.Attributes;
using System.ComponentModel.DataAnnotations;

namespace Contracts.Objects.Dtos.User
{
    public class UserPasswordUpdateDto
    {
        [Required(ErrorMessage = "Пароль обязателен")]
        [StrongPassword]
        public string Password { get; set; } = string.Empty;
    }
}
