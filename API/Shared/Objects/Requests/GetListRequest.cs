using Shared.Utilities.Validation;
using System.ComponentModel.DataAnnotations;

namespace Shared.Objects.Requests
{
    public class GetListRequest : CustomValidationObject
    {
        [Range(1, 100, ErrorMessage = "Limit должен быть больше 1 и меньше 101")]
        public int Limit { get; set; } = 50;

        [Range(1, int.MaxValue, ErrorMessage = "Offset должен быть больше 1")]
        public int Offset { get; set; } = 0;
        public string SearchTerm { get; set; } = "";
        public string OrderBy { get; set; } = "asc";

        [RegularExpression(@"^?(asc|desc|)$", ErrorMessage = "Направление сортировки должно быть asc или desc")]
        public string OrderDirection { get; set; } = "desc";
    }
}
