using Application.Types.Exceptions;
using Shared.App;
using System.Net;

namespace Application.Services.Command.Handlers
{
    internal class BanIpHandler : ICommandHandler
    {
        public string CommandName { get; } = "banip";

        public async Task<string> HandleAsync(string[] args)
        {
            if (args.Length == 0)
                throw new BadRequestException("использование: banip (add|remove|reload|show) (<ipv4>)");

            var cmd = args[0];

            if (cmd == "reload")
            {
                await AppCache.BanIp.LoadListAsync();
                return "banip лист перезагружен";
            }

            if (cmd == "show")
                return $"banip лист:\n{string.Join(", ", AppCache.BanIp.List)}";

            if (cmd == "add" || cmd == "remove")
            {
                if (args.Length < 2)
                    throw new BadRequestException("использование: (banip add|remove) <ipv4>");

                var ip = args[1];

                if (!IPAddress.TryParse(ip, out var addr) ||
                    addr.AddressFamily != System.Net.Sockets.AddressFamily.InterNetwork)
                    throw new BadRequestException("неверный формат ipv4");

                if (cmd == "add")
                {
                    if (AppCache.BanIp.List.Contains(ip))
                        throw new BadRequestException("ip уже в списке");

                    await AppCache.BanIp.AddIpAsync(ip);
                    return "banip список обновлён";
                }

                if (cmd == "remove")
                {
                    if (!AppCache.BanIp.List.Contains(ip))
                        throw new BadRequestException("ip не найден в списке");

                    await AppCache.BanIp.RemoveIpAsync(ip);
                    return "banip список обновлён";
                }
            }

            throw new BadRequestException("некорректный ввод");
        }
    }
}
