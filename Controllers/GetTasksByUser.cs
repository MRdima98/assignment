using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using assignment.Utilities;

namespace assignment.Controllers;

[ApiController]
[Route("[controller]")]
public class GetTasksByUserController : ControllerBase {
    private static bool USER_FILTER = true;

    public GetTasksByUserController() {
    }

    [HttpGet()]
    public async Task<List<AllTodos>> Get( [FromQuery] int limit,
        [FromQuery] int offset, [FromQuery] int userID) {

        List<Todos> todos = await DataUtility.FetchToDos();
        return DataUtility.FilterTodos(limit, offset, userID, USER_FILTER, todos);
    }
}

