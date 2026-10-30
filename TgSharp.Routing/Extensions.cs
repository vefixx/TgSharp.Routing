using Microsoft.Extensions.DependencyInjection;
using Telegram.BotAPI;

namespace TgSharp.Routing;

public static class Extensions
{
    public static IServiceCollection AddTelegramBot(this IServiceCollection services, TelegramBotClientOptions options)
    {
        TelegramBotClient botClient = new TelegramBotClient(options);
        
        services.AddSingleton<TelegramBotClient>(botClient);
        services.AddSingleton<IRouterProcessor, RouterProcessor>();
        services.AddSingleton<IUpdateProcessor, UpdateProcessor>();
        services.AddHostedService<Router>();

        return services;
    }
}