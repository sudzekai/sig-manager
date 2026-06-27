using Shared.App;
using System.Net;
using System.Text.Json;

namespace Application.Services.BanIp
{
    internal class BanIpService : IBanIpService
    {
        private readonly HashSet<string> _ipList = new();
        private readonly SemaphoreSlim _fileLock = new(1, 1);

        public IReadOnlyCollection<string> IpList => _ipList;

        public async Task LoadAsync()
        {
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
            ValidateIp(ip);

            lock (_ipList)
            {
                if (!_ipList.Add(ip))
                    throw new InvalidOperationException("IP already exists");
            }

            await SaveAsync();
        }

        public async Task RemoveAsync(string ip)
        {
            ValidateIp(ip);

            lock (_ipList)
            {
                if (!_ipList.Remove(ip))
                    throw new InvalidOperationException("IP not found");
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
                throw new ArgumentException("Invalid IPv4");
            }
        }
    }
}