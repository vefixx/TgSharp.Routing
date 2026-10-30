namespace TgSharp.Routing.Attributes;

[AttributeUsage(AttributeTargets.Method)]
public class OnSlashCommandAttribute : Attribute
{
    public OnSlashCommandAttribute(string command)
    {
        Command = command;
    }

    public string Command { get; }
}