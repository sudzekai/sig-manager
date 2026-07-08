using Domain.Exceptions;

namespace Domain.ValueObjects.Shifts
{
    public record ShiftType
    {
        public readonly string Value;

        private ShiftType(string value) => Value = value;

        public static ShiftType FromValue(string value)
            => value?.ToLower() switch
            {
                "car" => Car,
                "train" => Train,
                "popcorn" => Popcorn,
                "bouncer" => Bouncer,
                "carousel" => Carousel,
                "admin" => Admin,
                _ => throw new DataValidationException("Тип смены должен быть одним из: car, train, popcorn, bouncer, carousel, admin")
            };

        public static readonly ShiftType Car = new("car");

        public static readonly ShiftType Train = new("train");

        public static readonly ShiftType Popcorn = new("popcorn");
        
        public static readonly ShiftType Bouncer = new("bouncer");
        
        public static readonly ShiftType Carousel = new("carousel");
        
        public static readonly ShiftType Admin = new("admin");
    }
}
