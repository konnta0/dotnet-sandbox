using System.Collections.Concurrent;
using ServiceAgent.Demo;

namespace ServiceAgent;

internal sealed class ServiceAgentWorker(
    IServiceScopeFactory serviceScopeFactory, 
    ILogger<ServiceAgentWorker> logger) : BackgroundService, IServiceAgent<RoomServiceAgentContext, RoomServiceAgentParameter>, IAsyncDisposable
{
    private readonly ConcurrentDictionary<string, IExecutionServiceAgentContext> _agents = new();
    private CancellationTokenSource? _cts = new();

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _cts = CancellationTokenSource.CreateLinkedTokenSource(stoppingToken); 
        var timer = new PeriodicTimer(TimeSpan.FromSeconds(5));
        while (await timer.WaitForNextTickAsync(stoppingToken))
        {
            logger.LogInformation("ServiceAgentWorker is running");
        }
    }
    
    public async ValueTask StartAsync(string contextId, RoomServiceAgentParameter param, CancellationToken? cancellationToken = null)
    {
        if (_agents.TryGetValue(contextId, out _))
        {
            logger.LogWarning("Context {ContextId} is already running", contextId);
            return;
        }

        var scope = serviceScopeFactory.CreateScope();
        var context = new RoomServiceAgentContext(param, contextId, scope.ServiceProvider);
        if (!_agents.TryAdd(contextId, context))
        {
            logger.LogWarning("Context {ContextId} is already running", contextId);
            return;
        }

        var ctx = cancellationToken ?? _cts!.Token;
        try
        {
            context.StartAsync(ctx).Forget();
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Failed to start context {ContextId}", contextId);
            await context.StopAsync();
            _agents.TryRemove(contextId, out _);
            context.Dispose();
        }
    }

    public async ValueTask<bool> StopAsync(string contextId)
    {
        if (_agents.TryRemove(contextId, out var context))
        {
            try
            {
                await context.StopAsync();
                context.Dispose();
                return true;
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Failed to stop context {ContextId}", contextId);
                return false;
            }
        }

        logger.LogWarning("Context {ContextId} is not running", contextId);
        return false;
    }

    public bool IsRunning(string contextId)
    {
        if (_agents.TryGetValue(contextId, out var context))
        {
            return context.IsRunning;
        }
        logger.LogWarning("Context {ContextId} is not running", contextId);
        return false;
    }

    public RoomServiceAgentContext? GetContext(string contextId)
    {
        if (_agents.TryGetValue(contextId, out var context))
        {
            return context.As<RoomServiceAgentContext>();
        }
        logger.LogWarning("Context {ContextId} is not running", contextId);
        return null;
    }
    
    public RoomServiceAgentContext[] GetContexts()
    {
        return _agents.Values
            .Select(static x => x.As<RoomServiceAgentContext>())
            .ToArray();
    }

    public async ValueTask DisposeAsync()
    {
        await _cts!.CancelAsync();
        _cts.Dispose();
        _cts = null;

        foreach (var agent in _agents.Values)
        {
            await agent.StopAsync();
            agent.Dispose();
        }

        _agents.Clear();
    }
}

