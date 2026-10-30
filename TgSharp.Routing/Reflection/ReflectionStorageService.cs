using System.Collections.Concurrent;
using System.Reflection;
using TgSharp.Routing.Reflection.Interfaces;

namespace TgSharp.Routing.Reflection;

public class ReflectionStorageService : IReflectionStorageService
{
    private readonly ConcurrentDictionary<string, MethodInfo> _slashCommandHandlersStorage;

    public ReflectionStorageService()
    {
        _slashCommandHandlersStorage = new ConcurrentDictionary<string, MethodInfo>();
    }

    public void AddToSlashCommandHandlersStorage(string command, MethodInfo methodInfo)
    {
        if (!_slashCommandHandlersStorage.TryAdd(command, methodInfo))
        {
            throw new Exception(); // TODO: Create exception class
        }
    }

    public MethodInfo? GetSlashCommandHandlerFromStorage(string command)
    {
        return _slashCommandHandlersStorage.GetValueOrDefault(command);
    }
}