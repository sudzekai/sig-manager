using System.Data.Common;

namespace Infrastructure.Internal.Extensions
{
    internal static class DbDataReaderExtensions
    {
        public static int? GetNullableInt32(this DbDataReader reader, string columnName)
            => reader.GetNullableInt32(reader.GetOrdinal(columnName));

        public static int? GetNullableInt32(this DbDataReader reader, int ordinal)
            => reader.IsDBNull(ordinal) ? null : reader.GetInt32(ordinal);


        public static string? GetNullableString(this DbDataReader reader, string columnName)
            => reader.GetNullableString(reader.GetOrdinal(columnName));

        public static string? GetNullableString(this DbDataReader reader, int ordinal)
            => reader.IsDBNull(ordinal) ? null : reader.GetString(ordinal);


        public static DateTime? GetNullableDateTime(this DbDataReader reader, string columnName)
            => reader.GetNullableDateTime(reader.GetOrdinal(columnName));

        public static DateTime? GetNullableDateTime(this DbDataReader reader, int ordinal)
            => reader.IsDBNull(ordinal) ? null : reader.GetDateTime(ordinal);


        public static decimal? GetNullableDecimal(this DbDataReader reader, string columnName)
            => reader.GetNullableDecimal(reader.GetOrdinal(columnName));

        public static decimal? GetNullableDecimal(this DbDataReader reader, int ordinal)
            => reader.IsDBNull(ordinal) ? null : reader.GetDecimal(ordinal);
    }
}