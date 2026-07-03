using Contracts.Interfaces.Application.Services;
using Shared.App;
using Shared.Extensions;
using Shared.OpenTelemetry;
using System.Net;
using System.Text.Json;

namespace Application.Services
{
    public class BanIpService : IBanIpService
    {
        private readonly HashSet<string> _ipList = [];
        private readonly SemaphoreSlim _fileLock = new(1, 1);

        public IReadOnlyCollection<string> IpList => _ipList;

        public async Task LoadAsync()
        {
            using var activity = Telemetry.Service.StartRichActivity();

            var path = AppConstants.BanIpListFullPath;

            if (!File.Exists(path))
            {
                _ipList.Clear();
                return;
            }

            var json = await File.ReadAllTextAsync(path);

            var list = JsonSerializer.Deserialize<HashSet<string>>(json) ?? [];

            lock (_ipList)
            {
                _ipList.Clear();

                foreach (var ip in list)
                    _ipList.Add(ip);
            }
        }

        public async Task AddAsync(string ip)
        {
            using var activity = Telemetry.Service.StartRichActivity();

            ValidateIp(ip);

            lock (_ipList)
            {
                if (!_ipList.Add(ip))
                    throw new InvalidOperationException("IP уже существует");
            }

            await SaveAsync();
        }

        public async Task RemoveAsync(string ip)
        {
            using var activity = Telemetry.Service.StartRichActivity();

            ValidateIp(ip);

            lock (_ipList)
            {
                if (!_ipList.Remove(ip))
                    throw new InvalidOperationException("IP не найден");
            }

            await SaveAsync();
        }

        public bool Contains(string ip)
        {

            lock (_ipList)
            {
                return _ipList.Contains(ip);
            }
        }

        private async Task SaveAsync()
        {
            using var activity = Telemetry.Service.StartRichActivity();

            var path = AppConstants.BanIpListFullPath;

            await _fileLock.WaitAsync();
            try
            {
                var json = JsonSerializer.Serialize(_ipList, AppConstants.JsonOptionsPrettyWrite);

                await File.WriteAllTextAsync(path, json);
            }
            finally
            {
                _fileLock.Release();
            }
        }

        private static void ValidateIp(string ip)
        {
            if (!IPAddress.TryParse(ip, out var addr) ||
                addr.AddressFamily != System.Net.Sockets.AddressFamily.InterNetwork)
            {
                throw new ArgumentException("Некорректный IPv4");
            }
        }
    }
}
