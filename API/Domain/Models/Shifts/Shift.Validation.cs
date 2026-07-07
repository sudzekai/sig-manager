using Domain.Tools;

namespace Domain.Models.Shifts
{
    public partial class Shift
    {
        private void ValidateType(string type)
        {
            DataValidator.NullOrWhiteSpace(type, nameof(type));
            DataValidator.OneOf(type, ["car", "train", "popcorn", "bouncer", "carousel", "admin"], nameof(type));
        }

        private void ValidateStatus(string status)
        {
            DataValidator.NullOrWhiteSpace(status, nameof(status));
            DataValidator.OneOf(status, ["opened", "closed"], nameof(status));
        }
    }
}
