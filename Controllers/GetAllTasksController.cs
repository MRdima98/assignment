using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Threading.Tasks;
using System;
using System.Text.Json;

namespace assignment.Controllers;

[ApiController]
[Route("[controller]")]
public class GetAllTasksController : ControllerBase {

    public GetAllTasksController() {
    }

    static async Task<List<User>> FetchUsers() {
        using ( var httpClient = new HttpClient()){
            string api = "https://jsonplaceholder.typicode.com/users";
            var response = await httpClient.GetAsync(api);
            if (response.IsSuccessStatusCode) {
              var content = await response.Content.ReadAsStringAsync();
              List<User>? users = JsonSerializer.Deserialize<List<User>>(content);
              return users;
            }
        }
        return new List<User>();
    }

    static async Task<List<Todos>> FetchToDos (){
        using ( var httpClient = new HttpClient()){
            string api = "https://jsonplaceholder.typicode.com/todos";
            var response = await httpClient.GetAsync(api);
            if (response.IsSuccessStatusCode) {
              var content = await response.Content.ReadAsStringAsync();
              List<Todos>? todos = JsonSerializer.Deserialize<List<Todos>>(content);
              return todos;
            }
        }
        return new List<Todos> ();
    }


    [HttpGet()]
    public async Task<List<User>> Get([FromQuery]int limit,[FromQuery] int offset) {
        List<User> users = await FetchUsers();
        Console.WriteLine(offset);
        Console.WriteLine(limit);
        return await FetchUsers();
    }

}
