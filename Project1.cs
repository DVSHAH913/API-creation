// Program.cs
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton<List<User>>();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.MapGet("/users", (List<User> users) => Results.Ok(users));

app.MapGet("/users/{id}", (int id, List<User> users) =>
{
    var user = users.FirstOrDefault(u => u.Id == id);
    return user != null ? Results.Ok(user) : Results.NotFound();
});

app.MapPost("/users", (User user, List<User> users) =>
{
    users.Add(user);
    return Results.Created($"/users/{user.Id}", user);
});

app.MapPut("/users/{id}", (int id, User updatedUser, List<User> users) =>
{
    var user = users.FirstOrDefault(u => u.Id == id);
    if (user == null) return Results.NotFound();
    user.Name = updatedUser.Name;
    user.Email = updatedUser.Email;
    return Results.Ok(user);
});

app.MapDelete("/users/{id}", (int id, List<User> users) =>
{
    var user = users.FirstOrDefault(u => u.Id == id);
    if (user == null) return Results.NotFound();
    users.Remove(user);
    return Results.Ok();
});

app.Run();

public class User
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
}
