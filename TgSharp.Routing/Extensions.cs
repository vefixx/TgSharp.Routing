using Microsoft.Extensions.DependencyInjection;
using Telegram.BotAPI;
using TgSharp.Routing.Reflection;
using TgSharp.Routing.Reflection.Interfaces;

namespace TgSharp.Routing;

public static class Extensions
{
    public static IServiceCollection AddTelegramBot(this IServiceCollection services, TelegramBotClientOptions options,
        RouterOptions routerOptions)
    {
        TelegramBotClient botClient = new TelegramBotClient(options);
        
        services.AddSingleton<TelegramBotClient>(botClient);
        services.AddSingleton<RouterOptions>(routerOptions);
        services.AddSingleton<IReflectionStorageService, ReflectionStorageService>();
        services.AddSingleton<IReflectionRegisterService, ReflectionRegisterService>();
        services.AddSingleton<IReflectionInvokeService, ReflectionInvokeService>();
        services.AddSingleton<IDispatcher, Dispatcher>();
        services.AddSingleton<IUpdateProcessor, UpdateProcessor>();
        services.AddSingleton<IRouterProcessor, RouterProcessor>();
        services.AddHostedService<Router>();

        return services;
    }
}