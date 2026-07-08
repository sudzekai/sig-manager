using Domain.Models.Base;
using Domain.ValueObjects.Parks;

namespace Domain.Models.Parks
{
    public partial class Park : DomainModelBase
    {
        private Park(ParkId id, ParkName name)
        {
            Id = id; 
            Name = name;

            _initialized = true;
        }

        private Park(ParkName name)
        {
            Name = name;
            _initialized = true;
        }

        public static Park Restore(ParkId id, ParkName name) => new(id, name);

        public static Park Create(ParkName name) => new(name);
    }
}
