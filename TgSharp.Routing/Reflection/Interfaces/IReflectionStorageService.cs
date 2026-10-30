using System.Reflection;

namespace TgSharp.Routing.Reflection.Interfaces;

public interface IReflectionStorageService
{
    void AddToSlashCommandHandlersStorage(string command, MethodInfo methodInfo);
    MethodInfo? GetSlashCommandHandlerFromStorage(string command);
}