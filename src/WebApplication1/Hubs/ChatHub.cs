

using DFrameShared.Hubs;
using MagicOnion.Server.Hubs;

namespace WebApplication1.Hubs;

public class ChatHub : StreamingHubBase<IChatHub, IChatHubReceiver>, IChatHub
{
    IGroup<IChatHubReceiver>? room;
    string userName = "unknown";

    public async ValueTask JoinAsync(string roomName, string userName)
    {
        this.room = await Group.AddAsync(roomName);
        this.userName = userName;
        room.All.OnJoin(userName);
    }

    public async ValueTask LeaveAsync()
    {
        room.All.OnLeave(userName);
        await room.RemoveAsync(Context);
    }

    public async ValueTask SendMessageAsync(string message)
    {
        room.All.OnSendMessage(userName, message);
    }

}