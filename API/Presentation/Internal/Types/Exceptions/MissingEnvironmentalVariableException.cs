namespace Presentation.Internal.Types.Exceptions
{
    public class MissingEnvironmentalVariableException(string variableName) : Exception($"Отсутствует параметр среды: {variableName}");
}
