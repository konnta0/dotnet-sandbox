using CloudStructures;
using StackExchange.Redis;

namespace RedisPubSub;

internal static class RedisConnectionFactory
{
    public static RedisConnection Create()
    {
        var config = new RedisConfig("pubsub", new ConfigurationOptions
        {
            AbortOnConnectFail = false,
            Ssl = false,
            EndPoints = { "localhost:63799" }
        });
        return new RedisConnection(config);
    }
}