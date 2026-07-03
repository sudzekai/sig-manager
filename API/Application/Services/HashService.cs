using Contracts.Interfaces.Application.Services;
using Konscious.Security.Cryptography;
using Shared.Extensions;
using Shared.OpenTelemetry;
using System.Security.Cryptography;
using System.Text;

namespace Application.Services
{
    public class HashService : IHashService
    {
        public string HashString(string value)
        {
            using var activity = Telemetry.Service.StartRichActivity();

            byte[] salt = RandomNumberGenerator.GetBytes(16);

            var argon2 = new Argon2id(Encoding.UTF8.GetBytes(value))
            {
                Salt = salt,
                DegreeOfParallelism = 4,
                Iterations = 3,
                MemorySize = 65536
            };

            byte[] hash = argon2.GetBytes(32);

            return $"{Convert.ToBase64String(salt)}:{Convert.ToBase64String(hash)}";
        }

        public bool Compare(string value, string hashString)
        {
            using var activity = Telemetry.Service.StartRichActivity();

            string[] parts = hashString.Split(':');

            if (parts.Length != 2)
                return false;

            byte[] salt = Convert.FromBase64String(parts[0]);
            byte[] expectedHash = Convert.FromBase64String(parts[1]);

            var argon2 = new Argon2id(Encoding.UTF8.GetBytes(value))
            {
                Salt = salt,
                DegreeOfParallelism = 4,
                Iterations = 3,
                MemorySize = 65536
            };

            byte[] actualHash = argon2.GetBytes(expectedHash.Length);

            return CryptographicOperations.FixedTimeEquals(actualHash, expectedHash);
        }
    }
}
