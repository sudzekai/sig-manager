namespace Application.Services.BanIp
{
    public interface IBanIpService
    {
        IReadOnlyCollection<string> IpList { get; }

        Task AddAsync(string ip);
        bool Contains(string ip);
        Task LoadAsync();
        Task RemoveAsync(string ip);
    }
}