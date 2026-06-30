using System.ComponentModel.DataAnnotations;

namespace Contracts.Objects.Dtos.Models.Requests
{
    public class CommandDto
    {
        [Required(ErrorMessage = "Команда обязательна")]
        public string Command { get; set; } = string.Empty;
    }
}
