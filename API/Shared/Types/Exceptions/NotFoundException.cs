using System.Runtime.CompilerServices;

namespace Shared.Types.Exceptions
{
    public class NotFoundException(string businessMessage, string message, Exception? inner = null, [CallerFilePath] string filePath = "")
        : ApplicationException(businessMessage, $"{Path.GetFileNameWithoutExtension(filePath)}: {message}", inner)
    {
        public override int Code => 404;

        public static NotFoundException UserWithId(int id, [CallerFilePath] string filePath = "") 
            => new("Пользователь с таким идентификатором не найден", $"Запрошенный пользователь не найден id={id}", filePath: filePath);
        public static NotFoundException PositionWithId(int id, [CallerFilePath] string filePath = "") 
            => new("Должность с таким идентификатором не найдена", $"Запрошенная должность не найдена id={id}", filePath: filePath);

        public static NotFoundException CarWithId(int id, [CallerFilePath] string filePath = "") 
            => new("Машина с таким идентификатором не найден", $"Запрошенная машина не найдена id={id}", filePath: filePath);

        public static NotFoundException ShiftWithId(int id, [CallerFilePath] string filePath = "") 
            => new("Смена с таким идентификатором не найдена", $"Запрошенная смена не найдена id={id}", filePath: filePath);
    }
}
