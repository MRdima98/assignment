using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace assignment.Controllers;

[ApiController]
[Route("[controller]")]
public class GetTasksByUserController : ControllerBase {

    public GetTasksByUserController() {
    }

    static async Task<List<User>> FetchUsers() {
        using ( var httpClient = new HttpClient()){
            string api = "https://jsonplaceholder.typicode.com/users";
            var response = await httpClient.GetAsync(api);
            if (response.IsSuccessStatusCode) {
                var content = await response.Content.ReadAsStringAsync();
                List<User>? users = JsonSerializer.Deserialize<List<User>>(content);
                if (users != null) {
                    return users;
                }
            }
        }
        return new List<User>();
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
    public async Task<List<AllTodos>> Get( [FromQuery]int limit,
        [FromQuery] int offset, [FromQuery] int userID)
    {

        List<Todos> todos = await FetchToDos();
        List<User> users = await FetchUsers();
        List<AllTodos> allTodos = new List<AllTodos>();

        foreach (Todos item in todos) {
            if (userID != item.UserId) {
                continue;
            }
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

