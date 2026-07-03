using Contracts.Objects.Attributes;

namespace Contracts.Objects.Dtos.Car
{
    public class CarStatusUpdateDto
    {
        [OneOf("Working,Broken", ErrorMessage = "Status должен быть одним из: Working, Broken")]
        public string Status { get; set; } = string.Empty;
    }
}
