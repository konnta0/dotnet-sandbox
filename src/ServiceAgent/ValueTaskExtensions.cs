namespace ServiceAgent;

internal static class ValueTaskExtensions
{
    public static void Forget(this ValueTask valueTask)
    {
        if (!valueTask.IsCompletedSuccessfully)
        {
            _ = ForgetAwaited(valueTask);
        }
    }

    private static async Task ForgetAwaited(ValueTask valueTask)
    {
        try
        {
            await valueTask.ConfigureAwait(false);
        }
        catch (Exception ex)
        {
            throw;
        }
    }

    public static void Forget<T>(this ValueTask<T> valueTask)
    {
        if (!valueTask.IsCompletedSuccessfully)
        {
            _ = ForgetAwaited(valueTask);
        }
    }

    private static async Task ForgetAwaited<T>(ValueTask<T> valueTask)
    {
        try
        {
            await valueTask.ConfigureAwait(false);
        }
        catch (Exception ex)
        {
            throw;
        }
    }
}