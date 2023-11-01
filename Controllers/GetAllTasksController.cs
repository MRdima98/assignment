using Microsoft.AspNetCore.Mvc;
using assignment.Utilities;

namespace assignment.Controllers;

[ApiController]
[Route("[controller]")]
public class GetAllTasksController : ControllerBase {
    private static bool NO_USER_FILTER = false;
    private static int EMPTY_FIELD = 0;

    public GetAllTasksController() { }

    [HttpGet()]
    public async Task<IActionResult> Get([FromQuery] int limit, [FromQuery] int offset) {
        List<Todos> todos = await DataUtility.FetchToDos();
        List<User> users = await DataUtility.FetchUsers();
        List<TodosWithUser> return_data = DataUtility.
          FilterTodos(limit, offset, EMPTY_FIELD, NO_USER_FILTER, todos, users);
        if (return_data.Count() == 0) {
            return NotFound(return_data);
        }
        return Ok(return_data);
    }
}

