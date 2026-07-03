using Contracts.Objects.Attributes;
using System.ComponentModel.DataAnnotations;

namespace Contracts.Objects.Dtos.User
{
    public class UserCreateDto 
    {
        [Required(ErrorMessage = "Имя пользователя обязательно")]
        public string Username { get; set; } = string.Empty;

        [Required(ErrorMessage = "Имя обязательно")]
        public string FullName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Электронная почта обязательна")]
        [EmailAddress(ErrorMessage = "Некорректный формат электронной почты")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "Номер телефона обязателен")]
        [PhoneNumber]
        public string PhoneNumber { get; set; } = string.Empty;

        [Required(ErrorMessage = "Пароль обязателен")]
        [StrongPassword]
        public string Password { get; set; } = string.Empty;
    }
}
