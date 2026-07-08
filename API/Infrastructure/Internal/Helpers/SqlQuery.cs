namespace Infrastructure.Internal.Helpers
{
    internal static class SqlQuery
    {
        public const string SelectLastInsertId = "SELECT LAST_INSERT_ID()"; 

        public static string Select(string tableName, IEnumerable<string> columns)
        {
            if (string.IsNullOrWhiteSpace(tableName))
                throw new ArgumentException("Ошибка генерации sql.select: Название таблицы обязательно", nameof(tableName));

            var cols = columns.ToArray();

            ValidateColumns(cols, "select");

            return $"""
                    SELECT {string.Join(", ", cols)} 
                    FROM {tableName}
                    """;
        }

        public static string Select(string tableName, IEnumerable<string> columns, IEnumerable<string> whereKeys)
        {
            if (string.IsNullOrWhiteSpace(tableName))
                throw new ArgumentException("Ошибка генерации sql.select: Название таблицы обязательно", nameof(tableName));

            var cols = columns.ToArray();
            ValidateColumns(cols, "select");

            var keys = whereKeys.ToArray();
            ValidateKeys(keys, "select");

            if (keys.Intersect(cols).Any())
                throw new ArgumentException("Ошибка генерации sql.select: Названия колонок не могут совпадать с названиями ключей", nameof(whereKeys));


            return $"""
                    SELECT {string.Join(", ", cols)}
                    FROM {tableName}
                    WHERE
                        {string.Join("\n    OR ", keys.Select(x => $"{x} = @{x}"))};
                    """;
        }

        public static string Insert(string tableName, IEnumerable<string> columns)
        {
            if (string.IsNullOrWhiteSpace(tableName))
                throw new ArgumentException("Ошибка генерации sql.insert: Название таблицы обязательно", nameof(tableName));

            var cols = columns.ToArray();
            ValidateColumns(cols, "insert");

            return $"""
                    INSERT INTO {tableName} 
                        ({string.Join(", ", cols)})
                    VALUES 
                        (@{string.Join(", @", cols)});
                    """;
        }

        public static string Update(string tableName, IEnumerable<string> columns, IEnumerable<string> whereKeys)
        {
            if (string.IsNullOrWhiteSpace(tableName))
                throw new ArgumentException("Ошибка генерации sql.update: Название таблицы обязательно", nameof(tableName));

            var cols = columns.ToArray();
            ValidateColumns(cols, "update");

            var keys = whereKeys.ToArray();
            ValidateKeys(keys, "update");

            if (keys.Intersect(cols).Any())
                throw new ArgumentException("Ошибка генерации sql.update: Названия колонок не могут совпадать с названиями ключей", nameof(whereKeys));

            return $"""
                    UPDATE {tableName}
                    SET 
                        {string.Join(",\n    ", cols.Select(x => $"{x} = @{x}"))}
                    WHERE 
                        {string.Join("\n    OR ", keys.Select(x => $"{x} = @{x}"))};
                    """;
        }

        public static string Delete(string tableName, IEnumerable<string> whereKeys)
        {
            if (string.IsNullOrWhiteSpace(tableName))
                throw new ArgumentException("Ошибка генерации sql.update: Название таблицы обязательно", nameof(tableName));

            var keys = whereKeys.ToArray();
            ValidateKeys(keys, "delete");

            return $"""
                    DELETE FROM {tableName}
                    WHERE 
                        {string.Join("\n    OR ", keys.Select(x => $"{x} = @{x}"))};
                    """;
        }

        private static string[] ValidateColumns(string[] cols, string operation)
        {
            if (cols.Length == 0)
                throw new ArgumentException(
                    $"Ошибка генерации sql.{operation}: Количество колонок должно быть > 0",
                    "columns");

            return cols;
        }

        private static string[] ValidateKeys(string[] keys, string operation)
        {
            if (keys.Length == 0)
                throw new ArgumentException(
                    $"Ошибка генерации sql.{operation}: Количество ключей должно быть > 0",
                    "whereKeys");

            return keys;
        }
    }
}
