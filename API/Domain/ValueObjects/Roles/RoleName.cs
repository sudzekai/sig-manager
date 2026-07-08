using Domain.Exceptions;

namespace Domain.ValueObjects.Roles
{
    public record RoleName
    {
        public readonly string Value;

        private RoleName(string value) => Value = value;

        public static RoleName FromValue(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new DataValidationException("Название роли не может быть пустым");

            if (value.Length > 25)
                throw new DataValidationException("Название роли не может быть длиннее 25 символов");

            return new(value);
        }
    }
}
