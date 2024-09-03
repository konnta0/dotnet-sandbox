namespace RedisPubSub;

internal sealed class RedisContainer : IAsyncDisposable
{
    private Testcontainers.Redis.RedisContainer? _container;
    
    public async Task StartAsync()
    {
        _container = new Testcontainers.Redis.RedisBuilder()
            .WithPortBinding(63799, 6379)
            .Build();
        await _container.StartAsync();
    }

    public async ValueTask DisposeAsync()
    {
        if (_container != null) await _container.DisposeAsync();
    }
}