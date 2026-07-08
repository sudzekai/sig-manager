using Presentation.Internal.Types.Exceptions;

namespace Presentation.Internal.Utilities.Extensions
{
    internal static class ConfigurationManagerExtensions
    {
        public static string TryGetString(this ConfigurationManager config, string key)
            => (config[key] is var value && !string.IsNullOrWhiteSpace(value)) ? value : throw new MissingEnvironmentalVariableException(key);

        public static string? TryGetNullableString(this ConfigurationManager config, string key)
            => config[key] is not null ? config[key] : null;

        public static bool TryGetBool(this ConfigurationManager config, string key)
        {
            var value = config[key];

            if (string.IsNullOrWhiteSpace(value))
                    throw new MissingEnvironmentalVariableException(key);

            return value switch
            {
                "true" => true,
                "false" => false,
                _ => throw new MissingEnvironmentalVariableException(key)
            };
        }
            
    }
}
