using System.ComponentModel.DataAnnotations;

namespace Contracts.Objects.Dtos.CarShift
{
    public class CarShiftCloseDto
    {
        [Required(ErrorMessage = "Номер последнего билета обязателен")]
        public int LastTicket { get; set; }
        
        [Required(ErrorMessage = "Сумма наличных обязательна")]
        public decimal Cash { get; set; }
        
        [Required(ErrorMessage = "Сумма безанличных обязательна")]
        public decimal Cashless { get; set; }
    }
}
