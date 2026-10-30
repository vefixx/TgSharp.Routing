using System.Reflection;
using System.Reflection.Emit;
using TgSharp.Routing.Attributes;
using TgSharp.Routing.Reflection.Interfaces;

namespace TgSharp.Routing.Reflection;

public class ReflectionRegisterService : IReflectionRegisterService
{
    private static Assembly s_assembly;
    private readonly IReflectionStorageService _reflectionStorage;

    public ReflectionRegisterService(IReflectionStorageService reflectionStorage)
    {
        _reflectionStorage = reflectionStorage;
        s_assembly = Assembly.GetEntryAssembly()!;
    }

    public void RegisterAllHandlers()
    {
        RegisterSlashCommandHandlers();
    }

    private void RegisterSlashCommandHandlers()
    {
        MethodInfo[] slashCommandHandlerMethods = s_assembly.GetTypes()
            .SelectMany(t => t.GetMethods())
            .Where(m => m.GetCustomAttributes(typeof(OnSlashCommandAttribute), false).Length > 0)
            .ToArray();
        
        foreach (MethodInfo methodInfo in slashCommandHandlerMethods)
        {
            OnSlashCommandAttribute attribute =
                methodInfo.GetCustomAttribute<OnSlashCommandAttribute>()!;

            _reflectionStorage.AddToSlashCommandHandlersStorage(attribute.Command, methodInfo);
        }
    }
}