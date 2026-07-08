using MySql.Data.MySqlClient;
using System.Data.Common;

namespace Infrastructure.Internal.Extensions
{
    internal static class StringExtensions
    {
        public static string AsParameter(this string value) => $"@{value}";
        public static MySqlParameter ToMysqlParameter(this string key, object? value) => new($"@{key}", value) ;
    }
}
