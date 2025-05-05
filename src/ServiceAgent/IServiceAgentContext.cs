namespace ServiceAgent;

internal interface IServiceAgentContext : IDisposable
{
    string ContextId { get; }
    IServiceProvider ServiceProvider { get; }
}