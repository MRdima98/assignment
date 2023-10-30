namespace assignment;
using System.Text.Json.Serialization;

public class Todos {
    [JsonPropertyName("userId")]
    public int UserId { get; set;}

    [JsonPropertyName("id")]
    public int Id { get; set;}

    [JsonPropertyName("title")]
    public string? Title { get; set;}

    [JsonPropertyName("completed")]
    public bool Completed { get; set;}
}

public class AllTodos {
    public string? Title { get; set;}

    public bool Completed { get; set;}
}
