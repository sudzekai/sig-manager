using Presentation.Objects.Types.Exceptions;

namespace Presentation.Utilities.Extensions
{
    public static class ConfigurationManagerExtensions
    {
        public static string TryGetString(this ConfigurationManager config, string key)
            => (config[key] is var value && !string.IsNullOrWhiteSpace(value)) ? value : throw new MissingEnvironmentalVariableException(key);
    }
}
