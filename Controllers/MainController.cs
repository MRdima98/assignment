using Microsoft.AspNetCore.Mvc;

namespace assignment.Controllers;

[ApiController]
[Route("[controller]")]
public class MainController : ControllerBase {
    private static readonly string Gotem = "gotem";


    public MainController() {
    }

    [HttpGet()]
    public IEnumerable<string> Get() {
        return new string[] { "string" };
    }
}
