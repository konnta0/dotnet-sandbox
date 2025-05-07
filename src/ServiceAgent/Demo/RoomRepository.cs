namespace ServiceAgent.Demo;

internal sealed class RoomRepository(GameService gameService)
{
    public async ValueTask<Room> CreateRoomAsync(Room room)
    {
        await Task.Delay(1000);
        return room;
    }
    
    public async ValueTask<Room> GetRoomAsync(string id)
    {
        await Task.Delay(1000);
        return new Room(gameService)
        {
            Id = id,
            Name = "Room " + id,
            Description = "Description of room " + id,
            Capacity = 4
        };
    }
}