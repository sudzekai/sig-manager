using System.ComponentModel.DataAnnotations;

namespace Contracts.Objects.Dtos.Car
{
    public class CarCreateDto
    {
        [Required(ErrorMessage = "Название обязательно")]
        [StringLength(50, ErrorMessage = "Длина названия не может превышать 50 символов")]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "Номер обязателен")]
        [Range(1, int.MaxValue, ErrorMessage = "Номер должен быть больше 0")]
        public int Id { get; set; } = default;

        [Required(ErrorMessage = "Номер платы обязателен")]
        [StringLength(100, ErrorMessage = "Длина номера платы не может превышать 100 символов")]
        public string Plate { get; set; } = string.Empty;
    }
}
