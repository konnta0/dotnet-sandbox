namespace ServiceAgent.Demo;

internal sealed class Room
{
    private readonly GameService _gameService;
    public required string Id { get; init; }
    public required string Name { get; init; }
    public required string Description { get; init; }
    public required int Capacity { get; init; }
    
    public Game? Game { get; private set; }

    public Room(GameService gameService)
    {
        _gameService = gameService;
    }
    
    public async ValueTask StartAsync(CancellationToken cancellationToken)
    {
        await Task.Delay(1000, cancellationToken);
    }
    
    public async ValueTask StopAsync()
    {
        await Task.Delay(1000);
    }

    public async ValueTask StartGameAsync(string id, string name, string type, CancellationToken cancellationToken)
    {
        Game = await _gameService.CreateGameAsync(id, name, type);
        Game.StartAsync(cancellationToken).Forget();
    }
}