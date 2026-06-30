namespace Contracts.Interfaces.Application.Services
{
    public interface IHashService
    {
        bool Compare(string value, string hashString);
        string HashString(string value);
    }
}