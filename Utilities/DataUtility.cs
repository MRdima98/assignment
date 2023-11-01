namespace assignment.Utilities;
using System.Text.Json;

public class DataUtility {
    public static async Task<List<Todos>> FetchToDos() {
        using (var httpClient = new HttpClient()){
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

    public static async Task<List<User>> FetchUsers() {
        using (var httpClient = new HttpClient()){
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
    
    public static List<TodosWithUser> CombineData(List<User> users, List<Todos> todos) {
        List<TodosWithUser> todosWithUsers = new List<TodosWithUser>();
        foreach (Todos todo in todos) {
            TodosWithUser tmp = new TodosWithUser();
            tmp.Id = todo.Id;
            tmp.User = users.Find(user => user.Id == todo.UserId);
            tmp.Title = todo.Title; 
            tmp.Completed = todo.Completed;
            todosWithUsers.Add(tmp);
        }
        return todosWithUsers;
    }
    
    public static List<TodosWithUser> FilterTodos(int limit, int offset, int userID,
            bool filterUser, List<Todos> todos, List<User> users
            ) {
        List<TodosWithUser> todosWithUsers = new List<TodosWithUser>();
        todosWithUsers = CombineData(users, todos);

        if (filterUser) {
          todosWithUsers = todosWithUsers.Where(el => el?.User?.Id == userID).ToList();
        }
        
        if (offset > todosWithUsers.Count()) {
            return new List<TodosWithUser>();
        }

        if (offset > 0) {
            todosWithUsers.RemoveRange(0, offset);
        }

        if (limit > todosWithUsers.Count()) {
            return todosWithUsers;
        }

        if (limit > 0 && limit < todosWithUsers.Count()) {
            todosWithUsers.RemoveRange(limit, todosWithUsers.Count() - limit);
        }

        return todosWithUsers;
    }
}
