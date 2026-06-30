using System.Data;

namespace Infrastructure.Internal.Extensions
{
    internal static class DataReaderExtensions
    {
        public static bool TryGetOrdinal(this IDataReader reader, string columnName, out int ordinal)
        {
            try
            {
                ordinal = reader.GetOrdinal(columnName);
                return true;
            }
            catch
            {
                ordinal = -1;
                return false;
            }
        }

        public static int TryGetInt32(this IDataReader reader, string columnName)
        {
            if (!TryGetOrdinal(reader, columnName, out int ordinal))
                return default;

            return reader.TryGetInt32(ordinal);
        }

        public static int TryGetInt32(this IDataReader reader, int ordinal)
        {
            if (ordinal < 0)
                return default;
            return reader.IsDBNull(ordinal) ? default : reader.GetInt32(ordinal);
        }

        public static string? TryGetString(this IDataReader reader, string columnName)
        {
            if (!TryGetOrdinal(reader, columnName, out int ordinal))
                return null;

            return reader.TryGetString(ordinal);
        }

        public static string? TryGetString(this IDataReader reader, int ordinal)
        {
            if (ordinal < 0)
                return null;
            return reader.IsDBNull(ordinal) ? null : reader.GetString(ordinal);
        }

        public static DateTime TryGetDateTime(this IDataReader reader, string columnName)
        {
            if (!TryGetOrdinal(reader, columnName, out int ordinal))
                return default;

            return reader.TryGetDateTime(ordinal);
        }

        public static DateTime TryGetDateTime(this IDataReader reader, int ordinal)
        {
            if (ordinal < 0)
                return default;

            return reader.IsDBNull(ordinal) ? default : reader.GetDateTime(ordinal);
        }
    }
}
