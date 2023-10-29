using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Threading.Tasks;
using System;
using System.Text.Json;

namespace assignment.Controllers;

[ApiController]
[Route("[controller]")]
public class MainController : ControllerBase {

    public MainController() {
    }

    static async Task<List<User>> Fetch() {
        using ( var httpClient = new HttpClient()){
            string api = "https://jsonplaceholder.typicode.com/users";
            var response = await httpClient.GetAsync(api);
            if (response.IsSuccessStatusCode) {
              var content = await response.Content.ReadAsStringAsync();
              Console.WriteLine(content);
              List<User>? users = JsonSerializer.Deserialize<List<User>>(content);
              return users;
            }
        }
        return null;
    }

    [HttpGet()]
    public async Task<List<User>> Get() {
        await Fetch();
        return await Fetch();
    }
}
