namespace ServiceAgent.Demo;

internal sealed class Room
{
    public required string Id { get; init; }
    public required string Name { get; init; }
    public required string Description { get; init; }
    public required int Capacity { get; init; }
    
    
    public async ValueTask StartAsync(CancellationToken cancellationToken)
    {
        await Task.Delay(1000, cancellationToken);
    }
    
    public async ValueTask StopAsync()
    {
        await Task.Delay(1000);
    }
}