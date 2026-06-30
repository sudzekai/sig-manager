using System.ComponentModel.DataAnnotations;

namespace Presentation.Objects.Requests
{
    public class StringRoute
    {
        [Required(ErrorMessage = "Параметр маршрута обязателен")]
        public string Value { get; set; } = string.Empty;
    }
}
