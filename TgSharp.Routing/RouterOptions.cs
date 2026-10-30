namespace TgSharp.Routing;

public class RouterOptions
{
    public RouterOptions(bool skipPendingUpdates = false)
    {
        SkipPendingUpdates = skipPendingUpdates;
    }

    public bool SkipPendingUpdates { get; }
}