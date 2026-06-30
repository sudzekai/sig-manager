namespace Shared.Utilities.Extensions
{
    public static class ObjectExtensions
    {
        public static Dictionary<string, object?> ToLogBody(this object obj, string? prefix = null)
        {
            var result = new Dictionary<string, object?>();

            var props = obj.GetType().GetProperties();

            foreach (var prop in props)
            {
                var value = prop.GetValue(obj);

                if (value is null)
                    continue;

                var name = char.ToLowerInvariant(prop.Name[0]) + prop.Name[1..];

                result[prefix is null ? name : $"{prefix}.{name}"] = value;
            }

            return result;
        }
    }
}
