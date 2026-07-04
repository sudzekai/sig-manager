using System.Data;

namespace Infrastructure.Internal.Extensions
{
    internal static class DataReaderExtensions
    {
        public static int? GetNullableInt32(this IDataReader reader, string columnName)
            => reader.GetNullableInt32(reader.GetOrdinal(columnName));

        public static int? GetNullableInt32(this IDataReader reader, int ordinal)
            => reader.IsDBNull(ordinal) ? null : reader.GetInt32(ordinal);

        public static string? GetNullableString(this IDataReader reader, string columnName)
            => reader.GetNullableString(reader.GetOrdinal(columnName));

        public static string? GetNullableString(this IDataReader reader, int ordinal)
            => reader.IsDBNull(ordinal) ? null : reader.GetString(ordinal);

        public static DateTime? GetNullableDateTime(this IDataReader reader, string columnName)
            => reader.GetNullableDateTime(reader.GetOrdinal(columnName));

        public static DateTime? GetNullableDateTime(this IDataReader reader, int ordinal)
            => reader.IsDBNull(ordinal) ? null : reader.GetDateTime(ordinal);
    }
}
