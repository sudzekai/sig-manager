using System.ComponentModel.DataAnnotations;

namespace Contracts.Objects.Dtos.Car
{
    public class CarUpdateInfoDto
    {
        [StringLength(50, ErrorMessage = "Длина названия не может превышать 50 символов")]
        public string Name { get; private set; } = string.Empty;

        [Range(1, int.MaxValue, ErrorMessage = "Номер должен быть больше 0")]
        public int Number { get; private set; }

        [StringLength(100, ErrorMessage = "Длина номера платы не может превышать 100 символов")]
        public string Plate { get; private set; } = string.Empty;
    }
}
