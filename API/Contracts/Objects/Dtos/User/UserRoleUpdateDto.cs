using System.ComponentModel.DataAnnotations;

namespace Contracts.Objects.Dtos.User
{
    public class UserRoleUpdateDto
    {
        [Required(ErrorMessage = "Идентификатор роли обязателен")]
        [Range(1, int.MaxValue, ErrorMessage = "Идентификатор роли должен быть больше 0")]
        public int RoleId { get; set; } = default;
    }
}
