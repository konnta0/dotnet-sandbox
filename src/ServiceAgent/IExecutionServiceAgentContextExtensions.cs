namespace ServiceAgent;

internal static class IExecutionServiceAgentContextExtensions
{
    public static T As<T>(this IExecutionServiceAgentContext context) where T : class
    {
        if (context is T t)
        {
            return t;
        }

        throw new InvalidCastException($"Cannot cast {context.GetType()} to {typeof(T)}");
    }
}