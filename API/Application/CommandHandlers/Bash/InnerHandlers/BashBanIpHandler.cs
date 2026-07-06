using Application.CommandHandlers.Bash.Interfaces;
using Shared.App;
using Shared.Types.Exceptions;
using System.Net;

namespace Application.CommandHandlers.Bash.InnerHandlers
{
    internal class BashBanIpHandler : IBashCommandHandler
    {
        public string CommandName { get; } = "banip";

        public async Task<string> HandleAsync(string[] args)
        {
            if (args.Length == 0)
                throw new BadRequestException("использование: banip (add|remove|reload|show) (<ipv4>)", "banip command invalid input: args == 0");

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
                    throw new BadRequestException("использование: (banip add|remove) <ipv4>", "banip command invalid input: args < 2");

                var ip = args[1];

                if (!IPAddress.TryParse(ip, out var addr) ||
                    addr.AddressFamily != System.Net.Sockets.AddressFamily.InterNetwork)
                    throw new BadRequestException("неверный формат ipv4", "banip command invalid input: ipv4 validation error");

                if (cmd == "add")
                {
                    if (AppCache.BanIp.List.Contains(ip))
                        throw new BadRequestException("ip уже в списке", "banip command invalid input: ipv4 exists");

                    await AppCache.BanIp.AddIpAsync(ip);
                    return "banip список обновлён";
                }

                if (cmd == "remove")
                {
                    if (!AppCache.BanIp.List.Contains(ip))
                        throw new BadRequestException("ip не найден в списке", "banip command invalid input: ipv4 doesn't exist");

                    await AppCache.BanIp.RemoveIpAsync(ip);
                    return "banip список обновлён";
                }
            }

            throw new BadRequestException("некорректный ввод", "banip command invalid input");
        }
    }
}
