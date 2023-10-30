namespace assignment.Utilities;
using System.Text.Json;

public class DataUtility {
    public static async Task<List<Todos>> FetchToDos() {
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
    
    public static List<AllTodos> FilterTodos(
        int limit, int offset, int userID, bool filterUser, List<Todos> todos
        ) {
        List<AllTodos> allTodos = new List<AllTodos>();

        foreach (Todos item in todos) {
            if (userID != item.UserId && filterUser) {
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
