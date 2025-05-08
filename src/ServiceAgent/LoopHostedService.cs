using Cysharp.Threading;
using ServiceAgent.Demo;

namespace ServiceAgent;

internal sealed class LoopHostedService : IHostedService
{
    private readonly ILogicLooperPool _looperPool;
    private readonly IServiceAgent<RoomServiceAgentContext> _serviceAgent;
    private readonly ILogger _logger;

    public LoopHostedService(
        ILogicLooperPool looperPool,
        IServiceAgent<RoomServiceAgentContext> serviceAgent,
        ILogger<LoopHostedService> logger)
    {
        _looperPool = looperPool ?? throw new ArgumentNullException(nameof(looperPool));
        _serviceAgent = serviceAgent;
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public Task StartAsync(CancellationToken cancellationToken)
    {
        // Example: Register update action immediately.
        _ = _looperPool.RegisterActionAsync((in LogicLooperActionContext ctx) =>
        {
            if (ctx.CancellationToken.IsCancellationRequested)
            {
                // If LooperPool begins shutting down, IsCancellationRequested will be `true`.
                _logger.LogInformation(
                    "LoopHostedService will be shutdown soon. The registered action is shutting down gracefully.");
                return false;
            }

            foreach (var agent in _serviceAgent.GetContexts())
            {
                agent.UpdateAsync().Forget();
            }
            
            return true;
        });
        

        _logger.LogInformation(
            $"LoopHostedService is started. (Loopers={_looperPool.Loopers.Count}; TargetFrameRate={_looperPool.Loopers[0].TargetFrameRate:0}fps)");

        return Task.CompletedTask;
    }

    public async Task StopAsync(CancellationToken cancellationToken)
    {
        _logger.LogInformation("LoopHostedService is shutting down. Waiting for loops.");

        // Shutdown gracefully the LooperPool after 5 seconds.
        await _looperPool.ShutdownAsync(TimeSpan.FromSeconds(5));

        // Count remained actions in the LooperPool.
        var remainedActions = _looperPool.Loopers.Sum(x => x.ApproximatelyRunningActions);
        _logger.LogInformation($"{remainedActions} actions are remained in loop.");
    }
}