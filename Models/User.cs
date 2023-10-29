namespace assignment;
using System.Text.Json.Serialization;

public class User {
    /* public int TemperatureF => 32 + (int)(TemperatureC / 0.5556); */

    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("name")]
    public string? Name { get; set; }

    [JsonPropertyName("username")]
    public string? Username { get; set; }

    [JsonPropertyName("email")]
    public string? Email { get; set; }

    [JsonPropertyName("address")]
    public Address? Address { get; set; }

    [JsonPropertyName("phone")]
    public string? Phone { get; set; }

    [JsonPropertyName("website")]
    public string? Website { get; set; }

    [JsonPropertyName("company")]
    public Company? Company { get; set; }
}

public class Address {
    [JsonPropertyName("street")]
    public string? Street { get; set; }

    [JsonPropertyName("suite")]
    public string? Suite { get; set; }

    [JsonPropertyName("city")]
    public string? City { get; set; }

    [JsonPropertyName("zipcode")]
    public string? Zipcode { get; set; }
}

public class Geo {
    [JsonPropertyName("lat")]
    public string? Lat { get; set; }

    [JsonPropertyName("lng")]
    public string? Lng { get; set; }
}

public class Company {
    [JsonPropertyName("name")]
    public string? Name { get; set; }

    [JsonPropertyName("catchPhrase")]
    public string? CatchPhrase { get; set; }

    [JsonPropertyName("bs")]
    public string? Bs { get; set; }
}
