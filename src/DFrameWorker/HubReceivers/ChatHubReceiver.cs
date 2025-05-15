using DFrameShared.Hubs;

namespace DFrameWorker.HubReceivers;

class ChatHubReceiver : IChatHubReceiver
{
    public void OnJoin(string userName)
        => Console.WriteLine($"{userName} joined.");
    public void OnLeave(string userName)
        => Console.WriteLine($"{userName} left.");

    public void OnSendMessage(string userName, string message)
    {
        Console.WriteLine($"[{userName}]: {message}");
    }

    public void OnMessage(string userName, string message)
        => Console.WriteLine($"{userName}: {message}");
}