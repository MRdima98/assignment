using Microsoft.AspNetCore.Mvc;
using assignment.Utilities;

namespace assignment.Controllers;

[ApiController]
[Route("[controller]")]
public class GetTasksByUserController : ControllerBase {
    private static bool USER_FILTER = true;

    public GetTasksByUserController() { }

    [HttpGet()]
    public async Task<IActionResult> Get( [FromQuery] int limit,
        [FromQuery] int offset, [FromQuery] int userID) {

        List<Todos> todos = await DataUtility.FetchToDos();
        List<User> users = await DataUtility.FetchUsers();
        List<TodosWithUser> return_data = DataUtility.
          FilterTodos(limit, offset, userID, USER_FILTER, todos, users);
        if (return_data.Count() == 0) {
            return NotFound(return_data);
        }
        return Ok(return_data);
    }
}

