namespace Infrastructure.Internal.Types
{
    internal class SqlQueryException(string message) : Exception($"Ошибка генерации sql query: {message}");
}
