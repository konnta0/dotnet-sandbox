using System.Diagnostics.Contracts;

namespace ServiceAgent;

internal interface IServiceAgent<out T> where T : IExecutionServiceAgentContext
{
    ValueTask StartAsync(string contextId, object? param, CancellationToken? cancellationToken = null);
    ValueTask<bool> StopAsync(string contextId);
    bool IsRunning(string contextId);
    T? GetContext(string contextId);

    [Pure]
    T[] GetContexts();
}