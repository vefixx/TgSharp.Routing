namespace TgSharp.Routing;

public interface IRouterProcessor
{
    Task ProcessAsync(CancellationToken cancellationToken);
}