using Telegram.BotAPI;
using Telegram.BotAPI.AvailableMethods;
using Telegram.BotAPI.AvailableTypes;
using Telegram.BotAPI.Extensions.Commands;
using Telegram.BotAPI.GettingUpdates;
using TgSharp.Routing.Reflection.Interfaces;

namespace TgSharp.Routing;

public class Dispatcher : IDispatcher
{
    private readonly TelegramBotClient _botClient;
    private readonly IReflectionInvokeService _reflectionInvokeService;
    private string _botUsername;

    public Dispatcher(TelegramBotClient botClient, IReflectionInvokeService reflectionInvokeService)
    {
        _botClient = botClient;
        _reflectionInvokeService = reflectionInvokeService;
        _botUsername = "";
    }

    public async Task OnUpdateAsync(Update update, CancellationToken cancellationToken)
    {
        if (string.IsNullOrEmpty(_botUsername))
        {
            User me = await _botClient.GetMeAsync(cancellationToken: cancellationToken);
            _botUsername = me.Username;
        }
        
        try
        {
            if (update.Message != null)
            {
                await OnMessageAsync(update.Message, cancellationToken);
            }
        }
        catch (Exception exp)
        {
        }
    }

    private async Task OnMessageAsync(Message message, CancellationToken cancellationToken)
    {
        if (message.Entities?.Any(e => e.Type == "bot_command")
            ?? message.CaptionEntities?.Any(e => e.Type == "bot_command")
            ?? false)
        {
            (string command, string? args, string? username) = BotCommandParser.Parse(message.Text ?? message.Caption ?? string.Empty);
            await OnSlashCommandAsync(message, command, args, cancellationToken);
        }
    }

    private async Task OnSlashCommandAsync(Message message, string command, string? args,
        CancellationToken cancellationToken)
    {
        await _reflectionInvokeService.InvokeSlashCommandHandler(message, command, args, cancellationToken);
    }
}