
using CodeBridge.Services;
using Microsoft.AspNetCore.Mvc;

namespace CodeBridge.Controllers;

[ApiController]
public class HealthCheckController : ControllerBase
{
    [Route("ping")]
    [HttpGet]
    public IActionResult Ping()
    {
        return Ok($"{AssemblyHelper.GetAssemblyName()}. Version {AssemblyHelper.GetAssemblyVersion()}");
    }
}
