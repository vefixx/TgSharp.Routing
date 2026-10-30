using Microsoft.Extensions.Hosting;
using Telegram.BotAPI;
using Telegram.BotAPI.GettingUpdates;

namespace TgSharp.Routing;

public class Router : IHostedService
{
    private readonly IRouterProcessor _routerProcessor;

    public Router(IRouterProcessor routerProcessor)
    {
        _routerProcessor = routerProcessor;
    }
    
    public async Task StartAsync(CancellationToken cancellationToken)
    {
        while (true)
        {
            await _routerProcessor.ProcessAsync(cancellationToken);
        }
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }
}