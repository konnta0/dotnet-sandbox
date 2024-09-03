using StackExchange.Redis;

namespace RedisPubSub;

public sealed class RedisPubSubWorker(ILogger<RedisPubSubWorker> logger) : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken ca)
    {
        await using var redisContainer = new RedisContainer();
        await redisContainer.StartAsync();

        var connection = RedisConnectionFactory.Create();
        SetupLog(connection.GetConnection());
        var subscriber = connection.GetConnection().GetSubscriber();
        await subscriber.SubscribeAsync("pubsub-channel", HandleMessage);

        while (!ca.IsCancellationRequested)
        {
            await Task.Delay(1000, ca);
        }
    }

    private void HandleMessage(RedisChannel channel, RedisValue message)
    {
        try
        {
            logger.LogInformation("Received message: {message}", message);

            if (message == "stop")
            {
                logger.LogInformation("Stopping worker");
                StopAsync(CancellationToken.None);
            }
        } catch (Exception e)
        {
            logger.LogError(e, "Error handling message: {message}", message);
        }
    }

    private void SetupLog(ConnectionMultiplexer multiplexer)
    {
        multiplexer.ErrorMessage += (sender, args) => logger.LogError("ConnectionMultiplexer ErrorMessage: {message}", args.Message);
        multiplexer.InternalError += (sender, args) => logger.LogError("ConnectionMultiplexer InternalError: {exception}", args.Exception);
        multiplexer.ConnectionFailed += (sender, args) => logger.LogError("ConnectionMultiplexer ConnectionFailed: {endPoint}", args.EndPoint);
        multiplexer.ConnectionRestored += (sender, args) => logger.LogInformation("ConnectionMultiplexer ConnectionRestored: {endPoint}", args.EndPoint);
    }
}