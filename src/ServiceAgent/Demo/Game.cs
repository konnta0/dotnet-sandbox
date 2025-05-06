using Cysharp.Threading;

namespace ServiceAgent.Demo;

internal sealed class Game
{
    private readonly ILogger _logger;
    public required string Id { get; init; }
    public required string Name { get; init; }
    public required string Type { get; init; }

    public Game(ILogger logger)
    {
        _logger = logger;
    }
    
    public ValueTask StartAsync(CancellationToken cancellationToken)
    {
        using var looper = new LogicLooper(60);
        looper.RegisterActionAsync((in LogicLooperActionContext ctx) =>
        {
            _logger.LogInformation("Game {Id} is running", Id);
            return true;
        }).Forget();
        return ValueTask.CompletedTask;
    }
}