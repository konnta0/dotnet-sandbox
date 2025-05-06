namespace ServiceAgent.Demo;

internal sealed class GameService(ILogger<GameService> logger)
{
    public async ValueTask<Game> CreateGameAsync(string id, string name, string type)
    {
        await Task.Delay(100);
        var game = new Game(logger)
        {
            Id = id,
            Name = name,
            Type = type
        };
        return game;
    }
}