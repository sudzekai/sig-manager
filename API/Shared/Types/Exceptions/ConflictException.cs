using System.Runtime.CompilerServices;

namespace Shared.Types.Exceptions
{
    public class ConflictException
        (string businessMessage, string message, Exception? inner = null, [CallerFilePath] string filePath = "")
        : ApplicationException(businessMessage, $"{Path.GetFileNameWithoutExtension(filePath)}: {message}", inner)
    {
        public override int Code => 409;

        public static ConflictException UserPhoneNumber([CallerFilePath] string filePath = "")
            => new("Пользователь с таким номером телефона уже существует", "Пользователь с переданным phone_number уже существует", filePath: filePath);

        public static ConflictException UserUsername([CallerFilePath] string filePath = "")
            => new("Пользователь с таким именем уже существует", "Пользователь с переданным username уже существует", filePath: filePath);

        public static ConflictException UserEmail([CallerFilePath] string filePath = "")
            => new("Пользователь с такой электронной почтой уже существует", "Пользователь с переданным email уже существует", filePath: filePath);

        public static ConflictException CarName([CallerFilePath] string filePath = "")
            => new("Машина с таким названием уже существует", "Машина с переданным name уже существует", filePath: filePath);

        public static ConflictException CarId([CallerFilePath] string filePath = "")
            => new("Машина с таким идентификатором уже существует", "Машина с переданным id уже существует", filePath: filePath);
    }
}
