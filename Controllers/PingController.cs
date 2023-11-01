using Microsoft.AspNetCore.Mvc;
namespace assignment.Controllers;

[ApiController]
[Route("[controller]")]
public class PingController : ControllerBase {
    public PingController() { }

    [HttpGet()]
    public string Get() {
        return "Pong";
    }
}

