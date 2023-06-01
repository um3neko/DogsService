
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
        var response = $"{AssemblyHelper.GetAssemblyName()}. Version {AssemblyHelper.GetAssemblyVersion()}";
        return Ok(response);
    }
}
