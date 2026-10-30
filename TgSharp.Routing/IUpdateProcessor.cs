using Telegram.BotAPI.GettingUpdates;

namespace TgSharp.Routing;

public interface IUpdateProcessor
{
    Task ProcessAsync(Update update, CancellationToken cancellationToken);
}