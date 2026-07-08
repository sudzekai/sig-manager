namespace Shared.Types.Exceptions
{
    public class NotFoundException(string businessMessage, string message, Exception? inner = null) : ApplicationException(businessMessage, message, inner)
    {
        public override int Code => 404;

        public static NotFoundException UserWithId(int id) => new("Пользователь с таким идентификатором не найден", $"Запрошенный пользователь не найден id={id}");
        public static NotFoundException CarWithId(int id) => new("Машина с таким идентификатором не найден", $"Запрошенная машина не найдена id={id}");
        public static NotFoundException ShiftWithId(int id) => new("Смена с таким идентификатором не найдена", $"Запрошенная смена не найдена id={id}");
    }
}
