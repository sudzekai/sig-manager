using System.ComponentModel.DataAnnotations;

namespace Contracts.Objects.Dtos.Requests
{
    public abstract class GetListRequest
    {
        [Range(0, int.MaxValue, ErrorMessage = "Offset должен быть равен или больше нуля")]
        public int Offset { get; set; } = 0;

        [Range(1, 50, ErrorMessage = "Limit должен быть положительным числом и меньше 26")]
        public int Limit { get; set; } = 25;

        public string SearchTerm { get; set; } = string.Empty;

        public abstract string OrderBy { get; set; }

        [RegularExpression(@"^?(asc|desc|)$", ErrorMessage = "OrderDirection должен быть asc или desc")]
        public string OrderDirection { get; set; } = "desc";
    }
}
