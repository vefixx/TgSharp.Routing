using Microsoft.Extensions.Hosting;
using Telegram.BotAPI;
using Telegram.BotAPI.GettingUpdates;
using TgSharp.Routing.Reflection.Interfaces;

namespace TgSharp.Routing;

public class Router : IHostedService
{
    private readonly IRouterProcessor _routerProcessor;
    private readonly IReflectionRegisterService _reflectionRegisterService;

    public Router(IRouterProcessor routerProcessor, IReflectionRegisterService reflectionRegisterService)
    {
        _routerProcessor = routerProcessor;
        _reflectionRegisterService = reflectionRegisterService;
    }
    
    public async Task StartAsync(CancellationToken cancellationToken)
    {
        // Register handlers via System.Reflection
        _reflectionRegisterService.RegisterAllHandlers();
        
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