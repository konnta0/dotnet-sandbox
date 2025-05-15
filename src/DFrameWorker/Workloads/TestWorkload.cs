using System.Globalization;
using DFrame;
using DFrameShared;
using DFrameShared.Hubs;
using DFrameWorker.HubReceivers;
using Grpc.Net.Client;
using MagicOnion.Client;
using Microsoft.Extensions.Logging;

namespace DFrameWorker.Workloads;

[Workload(nameof(TestWorkload))]
public class TestWorkload : Workload
{
    private readonly ILogger<TestWorkload> _logger;
    private DateTime _beginTime;
    private DateTime _endTime;
    private readonly HttpClient _client;
    private readonly IMyFirstService _myFirstService;
    private IChatHub _chatHub;
    private readonly GrpcChannel _channel;
    private readonly ChatHubReceiver _receiver;

    public TestWorkload(ILogger<TestWorkload> logger)
    {
        _logger = logger;
        var controllerBaseAddress = Environment.GetEnvironmentVariable("APPLICATION_BASE_ADDRESS");
        if (string.IsNullOrEmpty(controllerBaseAddress))
        {
            throw new ArgumentException("invalid environment APPLICATION_BASE_ADDRESS");
        }
        _client = new HttpClient
        {
            BaseAddress = new Uri(controllerBaseAddress),
        };

        _channel = GrpcChannel.ForAddress("https://localhost:7023");
        _myFirstService = MagicOnionClient.Create<IMyFirstService>(_channel);
        _receiver = new ChatHubReceiver();
    }

    public override async Task SetupAsync(WorkloadContext context)
    {
        _beginTime = DateTime.Now;
        _chatHub = await StreamingHubClient.ConnectAsync<IChatHub, IChatHubReceiver>(_channel, _receiver);
    }

    public override async Task ExecuteAsync(WorkloadContext context)
    {
        _endTime = DateTime.Now;
        await _chatHub.JoinAsync("room", "user1");
        await _chatHub.SendMessageAsync("Hello, world!"); 
        
        var result = await _myFirstService.SumAsync(123, 456);
        _logger.LogInformation("response content: {content}",result);
        return;
        
        var message = await _client.GetAsync("/get");
        var content = await message.Content.ReadAsStringAsync();
        _logger.LogInformation("response content: {content}",content);
    }

    public override Dictionary<string, string>? Complete(WorkloadContext context)
    {
        _client.Dispose();
        return new()
        {
            { "begin", _beginTime.ToString(CultureInfo.InvariantCulture) },
            { "end", _endTime.ToString(CultureInfo.InvariantCulture) },
        };
    }
}