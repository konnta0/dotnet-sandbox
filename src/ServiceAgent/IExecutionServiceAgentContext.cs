namespace ServiceAgent;

internal interface IExecutionServiceAgentContext : IServiceAgentContext
{
    ValueTask StartAsync(CancellationToken cancellationToken);
    ValueTask StopAsync();
    bool IsRunning { get; }
}