using Telegram.BotAPI;
using Telegram.BotAPI.GettingUpdates;

namespace TgSharp.Routing;

public class RouterProcessor : IRouterProcessor
{
    private readonly TelegramBotClient _botClient;
    private readonly IUpdateProcessor _updateProcessor;
    private int? _offset;
    private bool _firstExecute;
    private IEnumerable<Update> _updates;

    public RouterProcessor(TelegramBotClient botClient, IUpdateProcessor updateProcessor)
    {
        _botClient = botClient;
        _updateProcessor = updateProcessor;
        _offset = null;
        _updates = [];
        _firstExecute = true;
    }
    
    /// <summary>
    /// Выполняет запрос к просмотру событий бота
    /// </summary>
    /// <param name="cancellationToken"></param>
    public async Task ProcessAsync(CancellationToken cancellationToken)
    {
        // Если это первый запуск, то сливаем всю очередь накопившихся событий
        if (_firstExecute)
        {
            IEnumerable<Update> pendingUpdates;
            do
            {
                pendingUpdates = await _botClient.GetUpdatesAsync(offset: _offset, cancellationToken: cancellationToken);
                if (pendingUpdates.Any())
                {
                    // Сдвигаем offset, чтобы отметить их как прочитанные, но не обрабатываем
                    _offset = pendingUpdates.Last().UpdateId + 1;
                }
            } 
            while (pendingUpdates.Any()); // Повторяем, пока не обработаем все события, т.к. limit максимум 100
            
            _firstExecute = false;
        }
        
        // Получаем только новые события
        _updates = await _botClient.GetUpdatesAsync(offset: _offset, cancellationToken: cancellationToken);

        if (_updates.Any())
        {
            foreach (Update update in _updates)
            {
                await _updateProcessor.ProcessAsync(update, cancellationToken);
            }
            
            _offset = _updates.Last().UpdateId + 1;
        }
    }
}