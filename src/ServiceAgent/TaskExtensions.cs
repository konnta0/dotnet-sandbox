namespace ServiceAgent;

internal static class TaskExtensions
{
    public static void Forget(this Task task)
    {
        if (!task.IsCompletedSuccessfully)
        {
            _ = ForgetAwaited(task);
        }
    }

    private static async Task ForgetAwaited(Task task)
    {
        try
        {
            await task.ConfigureAwait(false);
        }
        catch (Exception ex)
        {
            throw;
        }
    }

    public static void Forget<T>(this Task<T> task)
    {
        if (!task.IsCompletedSuccessfully)
        {
            _ = ForgetAwaited(task);
        }
    }

    private static async Task ForgetAwaited<T>(Task<T> task)
    {
        try
        {
            await task.ConfigureAwait(false);
        }
        catch (Exception ex)
        {
            throw;
        }
    }
}