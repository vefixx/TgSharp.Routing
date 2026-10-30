using System.Reflection;
using Telegram.BotAPI.AvailableTypes;
using TgSharp.Routing.Reflection.Interfaces;

namespace TgSharp.Routing.Reflection;

public class ReflectionInvokeService : IReflectionInvokeService
{
    private readonly IReflectionStorageService _reflectionStorageService;
    
    public ReflectionInvokeService(IReflectionStorageService reflectionStorageService)
    {
        _reflectionStorageService = reflectionStorageService;
    }
    
    public async Task InvokeSlashCommandHandler(Message message, string command, string? args, CancellationToken cancellationToken)
    {
        MethodInfo? methodInfo = _reflectionStorageService.GetSlashCommandHandlerFromStorage(command);
        Console.WriteLine();
    }
}