namespace ServiceAgent.Demo;

internal class RoomServiceAgentContext : IExecutionServiceAgentContext
{
    private readonly RoomServiceAgentParameter _parameter;
    public string RoomId => ContextId;
    public readonly Room Room;
    
    public string ContextId { get; }

    public IServiceProvider ServiceProvider { get; }

    public RoomServiceAgentContext(RoomServiceAgentParameter parameter, string contextId, IServiceProvider serviceProvider)
    {
        _parameter = parameter;
        ContextId = contextId;
        ServiceProvider = serviceProvider;
        var gameService = ServiceProvider.GetRequiredService<GameService>();
        Room = new Room(gameService)
        {
            Id = contextId,
            Name = parameter.Name,
            Description = parameter.Description,
            Capacity = parameter.Capacity
        };
    }

    public async ValueTask StartAsync(CancellationToken cancellationToken)
    {
        var roomService = ServiceProvider.GetRequiredService<RoomService>();
        await roomService.CreateAsync(Room);
        await Room.StartAsync(cancellationToken);
        
        IsRunning = true;
    }

    public async ValueTask StopAsync()
    {
        var roomService = ServiceProvider.GetRequiredService<RoomService>();
        await roomService.RemoveAsync(Room);
        IsRunning = false;
    }

    public bool IsRunning { get; private set; }
    
    public void Dispose()
    {
    }
}