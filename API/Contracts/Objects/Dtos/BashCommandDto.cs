using System.ComponentModel.DataAnnotations;

namespace Contracts.Objects.Dtos
{
    public class BashCommandDto
    {
        [Required(ErrorMessage = "Команда обязательна")]
        public string Command { get; set; } = string.Empty;
    }
}
