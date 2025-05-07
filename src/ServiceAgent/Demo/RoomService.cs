namespace ServiceAgent.Demo;

internal sealed class RoomService(RoomRepository roomRepository)
{
    public async ValueTask CreateAsync(Room room)
    {
        await roomRepository.CreateRoomAsync(room);
    }

    public async ValueTask RemoveAsync(Room room)
    {
        await Task.Delay(1000);
    }
}