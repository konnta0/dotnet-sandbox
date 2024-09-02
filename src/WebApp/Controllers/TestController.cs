namespace WebApp.Controllers;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/v{version:apiVersion}/[controller]")]
public sealed class TestController(ILogger<TestController> logger) : ControllerBase
{
    [HttpGet("no-param")]
    public IActionResult Get()
    {
        logger.LogInformation("Called ping");
        return Ok("result");
    }
    
    [HttpGet("with-route-param/{input}")]
    public IActionResult Get([FromRoute]string input)
    {
        logger.LogInformation("Called ping: {input}", input);
        return Ok("result");
    }
}