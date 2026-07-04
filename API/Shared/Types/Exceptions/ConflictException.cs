namespace Shared.Types.Exceptions
{
    public class ConflictException(string businessMessage, string message, Exception? inner = null) : ApplicationException(businessMessage, message, inner)
    {
        public override int Code => 409;

        public static ConflictException UserPhoneNumber => new("Пользователь с таким номером телефона уже существует", "Пользователь с переданным phone_number уже существует");
        public static ConflictException UserUsername => new("Пользователь с таким именем уже существует", "Пользователь с переданным username уже существует");
        public static ConflictException UserEmail => new("Пользователь с такой электронной почтой уже существует", "Пользователь с переданным email уже существует");

        public static ConflictException CarName => new("Машина с таким названием уже существует", "Машина с переданным name уже существует");
        public static ConflictException CarNumber => new("Машина с таким номером уже существует", "Машина с переданным number уже существует");
    }
}
