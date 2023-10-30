using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace assignment.Controllers;

[ApiController]
[Route("[controller]")]
public class GetAllTasksController : ControllerBase {

    public GetAllTasksController() {
    }

    static async Task<List<Todos>> FetchToDos() {
        using ( var httpClient = new HttpClient()){
            string api = "https://jsonplaceholder.typicode.com/todos";
            var response = await httpClient.GetAsync(api);
            if (response.IsSuccessStatusCode) {
                var content = await response.Content.ReadAsStringAsync();
                List<Todos>? todos = JsonSerializer.Deserialize<List<Todos>>(content);
                if (todos != null) {
                  return todos;
                }
            }
        }
        return new List<Todos> ();
    }


    [HttpGet()]
    public async Task<List<AllTodos>> Get([FromQuery]int limit,[FromQuery] int offset) {
        List<Todos> todos = await FetchToDos();
        List<AllTodos> allTodos = new List<AllTodos>();


        foreach (Todos item in todos) {
            AllTodos tmp = new AllTodos();
            tmp.Title = item.Title;
            tmp.Completed = item.Completed;
            allTodos.Add(tmp);
        }

        if (offset > allTodos.Count()) {
            return new List<AllTodos>();
        }


        if (offset > 0) {
            allTodos.RemoveRange(0, offset);
        }

        if (limit > 0 && limit < allTodos.Count()) {
            allTodos.RemoveRange(limit, allTodos.Count() - limit);
        }

        return allTodos;
    }
}

