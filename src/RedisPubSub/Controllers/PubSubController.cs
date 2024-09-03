using Microsoft.AspNetCore.Mvc;

namespace RedisPubSub.Controllers;

[ApiController]
[Route("[controller]")]
public class PubSubController(ILogger<PubSubController> logger) : ControllerBase
{
    [HttpPost]
    public IActionResult AddMessage([FromBody]string message)
    {
        var connection = RedisConnectionFactory.Create();
        connection.GetConnection().GetSubscriber().Publish("pubsub-channel", message);
        logger.LogInformation("Published message: {message}", message);
        return Ok(message);
    }
}