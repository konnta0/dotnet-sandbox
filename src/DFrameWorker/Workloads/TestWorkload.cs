using System.Globalization;
using DFrame;
using Microsoft.Extensions.Logging;

namespace Worker.Workloads;

[Workload(nameof(TestWorkload))]
public class TestWorkload : Workload
{
    private readonly ILogger<TestWorkload> _logger;
    private DateTime _beginTime;
    private DateTime _endTime;
    private readonly HttpClient _client;

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
    }

    public override Task SetupAsync(WorkloadContext context)
    {
        _beginTime = DateTime.Now;

        return Task.CompletedTask;
    }

    public override async Task ExecuteAsync(WorkloadContext context)
    {
        _endTime = DateTime.Now;
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