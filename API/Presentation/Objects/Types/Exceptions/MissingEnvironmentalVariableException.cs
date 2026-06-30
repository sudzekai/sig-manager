namespace Presentation.Objects.Types.Exceptions
{
    public class MissingEnvironmentalVariableException(string variableName) : Exception($"Отсутствует параметр среды: {variableName}")
    {

    }
}
