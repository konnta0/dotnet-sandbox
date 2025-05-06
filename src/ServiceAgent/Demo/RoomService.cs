namespace ServiceAgent.Demo;

internal sealed class RoomService
{
    public async ValueTask CreateAsync(Room room)
    {
        await Task.Delay(1000);
    }

    public async ValueTask RemoveAsync(Room room)
    {
        await Task.Delay(1000);
    }
}