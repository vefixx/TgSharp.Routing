using Telegram.BotAPI.GettingUpdates;

namespace TgSharp.Routing;

public class UpdateProcessor : IUpdateProcessor
{
    private readonly IDispatcher _dispatcher;

    public UpdateProcessor(IDispatcher dispatcher)
    {
        _dispatcher = dispatcher;
    }

    public async Task ProcessAsync(Update update, CancellationToken cancellationToken)
    {
        await _dispatcher.OnUpdateAsync(update, cancellationToken);
    }
}