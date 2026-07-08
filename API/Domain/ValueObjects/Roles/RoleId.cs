using Domain.Exceptions;

namespace Domain.ValueObjects.Roles
{
    public record RoleId
    {
        public readonly int Value;

        private RoleId(int value) => Value = value;

        public static RoleId FromValue(int value)
        {
            if (value < 1)
                throw new DataValidationException("Идентификатор роли не может быть меньше 1");

            return new(value);
        }
    }
}
