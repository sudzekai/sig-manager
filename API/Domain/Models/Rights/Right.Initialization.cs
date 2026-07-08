using Domain.Models.Base;
using Domain.ValueObjects.Rights;

namespace Domain.Models.Rights
{
    public partial class Right : DomainModelBase
    {
        private Right(RightId id, RightCode code)
        {
            Id = id;
            Code = code;

            _initialized = true;
        }

        private Right(RightCode code)
        {
            Code = code;

            _initialized = true;
        }

        public static Right Restore(RightId id, RightCode code)
            => new(id, code);

        public static Right Create(RightCode code)
            => new(code);
    }
}
