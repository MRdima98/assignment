using Microsoft.VisualStudio.TestTools.UnitTesting;
using assignment.Utilities;
using assignment;
using Bogus;
using System.Text.Json;


[TestClass]
public class DataUtilityTest {
    List<User> users = new List<User>();
    List<Todos> todos = new List<Todos>();
    List<TodosWithUser> todosWithUsers = new List<TodosWithUser>();
    private int GOOD_OFFSET = 1;
    private int BAD_OFFSET = 10;
    private int GOOD_LIMIT = 1;
    private int BAD_LIMIT = 10;
    private int INVALID_USER_ID = 1001;
    private int SIZE = 5;

    [TestInitialize]
    public void Initialize() {
      var faker = new Faker("en");

      for (int i=0; i< SIZE; i++) {
          User user = genUsers();
          users.Add(user);
          Todos todo = genTodos(user);
          todos.Add(todo);
          TodosWithUser todosWithUser = genTodosWithUser(user, todo);
          todosWithUsers.Add(todosWithUser);
      }
    }

    [TestMethod]
    public void TestCombineData() {
        List<TodosWithUser> test = DataUtility.CombineData(users, todos);
        Assert.AreEqual(JsonSerializer.Serialize(test), 
            JsonSerializer.Serialize(todosWithUsers));
    }

    [TestMethod]
    public void TestGoodLimitFilter() {
        List<TodosWithUser> test = DataUtility.FilterTodos(GOOD_LIMIT, 0, 0, false, todos, users);
        todosWithUsers.RemoveRange(GOOD_LIMIT, todosWithUsers.Count() - GOOD_LIMIT);
        Assert.AreEqual(JsonSerializer.Serialize(test), 
            JsonSerializer.Serialize(todosWithUsers));
    }

    [TestMethod]
    public void TestBadLimitFilter() {
        List<TodosWithUser> test = DataUtility.FilterTodos(BAD_LIMIT, 0, 0, false, todos, users);
        Assert.AreEqual(JsonSerializer.Serialize(test), 
            JsonSerializer.Serialize(todosWithUsers));
    }

    [TestMethod]
    public void TestGoodOffsetFilter() {
        List<TodosWithUser> test = DataUtility.FilterTodos(0, GOOD_OFFSET, 0, false, todos, users);
        todosWithUsers.RemoveRange(0, GOOD_OFFSET);
        Assert.AreEqual(JsonSerializer.Serialize(test), 
            JsonSerializer.Serialize(todosWithUsers));
    }

    [TestMethod]
    public void TestBadOffsetFilter() {
        List<TodosWithUser> test = DataUtility.FilterTodos(0, BAD_OFFSET, 0, false, todos, users);
        Assert.AreEqual(JsonSerializer.Serialize(test), 
            JsonSerializer.Serialize(new List<TodosWithUser>()));
    }

    [TestMethod]
    public void TestValidingUserFilter() {
        List<TodosWithUser> test = DataUtility.FilterTodos(0, 
            0, users[0].Id, true, todos, users);
        Assert.AreEqual(JsonSerializer.Serialize(test), 
            JsonSerializer.Serialize(todosWithUsers.Where(el => el?.User?.Id == users[0].Id)));
    }
    
    [TestMethod]
    public void TestInvalidingUserFilter() {
        List<TodosWithUser> test = DataUtility.FilterTodos(0, 
            0, INVALID_USER_ID, true, todos, users);
        Assert.AreEqual(JsonSerializer.Serialize(test), 
            JsonSerializer.Serialize(new List<TodosWithUser>()));
    }

    public User genUsers() {
        var testGeo = new Faker<Geo>()
          .StrictMode(true)
          .RuleFor(geo => geo.Lat, f => f.Address.Latitude().ToString())
          .RuleFor(geo => geo.Lng, f => f.Address.Longitude().ToString());

        var testAddress = new Faker<Address>()
          .StrictMode(true)
          .RuleFor(address => address.Street, f => f.Address.StreetAddress())
          .RuleFor(address => address.Suite, f => f.Random.Word())
          .RuleFor(address => address.City, f => f.Address.City())
          .RuleFor(address => address.Geo, f => testGeo.Generate())
          .RuleFor(address => address.Zipcode, f => f.Address.ZipCode());

        var testCompany = new Faker<Company>()
          .StrictMode(true)
          .RuleFor(company => company.Name, f => f.Company.CompanyName())
          .RuleFor(company => company.CatchPhrase, f => f.Company.CatchPhrase())
          .RuleFor(company => company.Bs, f => f.Company.Bs())
          ;

        var testUsers = new Faker<User>()
          .StrictMode(true)
          .RuleFor(user => user.Id, f => f.Random.Int(1,1000))
          .RuleFor(user => user.Name, f => f.Name.FirstName())
          .RuleFor(user => user.Username, f => f.Internet.UserName())
          .RuleFor(user => user.Email, f => f.Internet.Email())
          .RuleFor(user => user.Address, f => testAddress.Generate())
          .RuleFor(user => user.Phone, f => f.Phone.PhoneNumber())
          .RuleFor(user => user.Website, f => f.Random.AlphaNumeric(12))
          .RuleFor(user => user.Company, f => testCompany.Generate());
        return testUsers.Generate();
    }

    public Todos genTodos(User user) {
        var todosTest = new Faker<Todos>()
          .StrictMode(true)
          .RuleFor(todos => todos.Id, f => f.Random.Int(1,1000))
          .RuleFor(todos => todos.UserId, f => user.Id)
          .RuleFor(todos => todos.Title, f => f.Random.AlphaNumeric(12))
          .RuleFor(todos => todos.Completed, f => f.Random.Bool());
        return todosTest.Generate();
    }

    public TodosWithUser genTodosWithUser(User user, Todos todos) {
        var testTodosWithUser = new Faker<TodosWithUser>()
          .StrictMode(true)
          .RuleFor(todos => todos.Id, f => todos.Id)
          .RuleFor(todos => todos.User, f => user)
          .RuleFor(todos => todos.Title, f => todos.Title)
          .RuleFor(todos => todos.Completed, f => todos.Completed);
        return testTodosWithUser.Generate();
    }
}

