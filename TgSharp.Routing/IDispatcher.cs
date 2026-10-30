using Telegram.BotAPI.GettingUpdates;

namespace TgSharp.Routing;

public interface IDispatcher
{
    Task OnUpdateAsync(Update update, CancellationToken cancellationToken);
}