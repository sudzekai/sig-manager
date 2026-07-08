using System.ComponentModel.DataAnnotations;

namespace Presentation.Internal.Objects.Requests
{
    public class IdRoute
    {
        [Required(ErrorMessage = "Идентификатор обязателен")]
        [Range(1, int.MaxValue, ErrorMessage = "Идентификатор должен быть больше 0")]
        public int Id { get; set; }
    }
}
