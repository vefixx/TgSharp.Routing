using Microsoft.Extensions.DependencyInjection;
using Telegram.BotAPI;

namespace TgSharp.Routing;

public static class Extensions
{
    public static IServiceCollection AddTelegramBot(this IServiceCollection services, TelegramBotClientOptions options,
        RouterOptions routerOptions)
    {
        TelegramBotClient botClient = new TelegramBotClient(options);
        
        services.AddSingleton<TelegramBotClient>(botClient);
        services.AddSingleton<RouterOptions>(routerOptions);
        services.AddSingleton<IRouterProcessor, RouterProcessor>();
        services.AddSingleton<IUpdateProcessor, UpdateProcessor>();
        services.AddHostedService<Router>();

        return services;
    }
}