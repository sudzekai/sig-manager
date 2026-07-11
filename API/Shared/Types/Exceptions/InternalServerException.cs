using System.Runtime.CompilerServices;

namespace Shared.Types.Exceptions
{
    public class InternalServerException(string businessMessage, string message, Exception? inner = null, [CallerFilePath] string filePath = "")
        : ApplicationException(businessMessage, $"{Path.GetFileNameWithoutExtension(filePath)}: {message}", inner)
    {
        public override int Code => 500;
    }
}
