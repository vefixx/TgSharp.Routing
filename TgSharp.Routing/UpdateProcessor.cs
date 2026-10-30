using Telegram.BotAPI.GettingUpdates;

namespace TgSharp.Routing;

public class UpdateProcessor : IUpdateProcessor                                         
{
    public Task ProcessAsync(Update update, CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }
}