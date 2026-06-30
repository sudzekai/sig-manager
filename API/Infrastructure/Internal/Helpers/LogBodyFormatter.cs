using MySql.Data.MySqlClient;

namespace Infrastructure.Internal.Helpers
{
    internal static class LogBodyFormatter
    {
        public static Dictionary<string, object?> FormatSqlExecution(string query, MySqlParameter[] parameters)
        {
            List<(string Key, object? Value)> result =
                [("query", GetNormalizedQuery(query)), .. parameters.Select(x => ($"query.{x.ParameterName.TrimStart('@')}", x.Value))];

            return result.ToDictionary(x => x.Key, x => x.Value);
        }

        private static string GetNormalizedQuery(string query)
        {
            query = query.Trim().Replace("\n", " ").Replace("\r", " ");

            while (query.Contains("  "))
                query = query.Replace("  ", " ");

            return query;
        }
    }
}
