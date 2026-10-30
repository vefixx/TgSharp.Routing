using Telegram.BotAPI.AvailableTypes;

namespace TgSharp.Routing.Reflection.Interfaces;

public interface IReflectionInvokeService
{
    Task InvokeSlashCommandHandler(Message message, string command, string? args, CancellationToken cancellationToken);
}