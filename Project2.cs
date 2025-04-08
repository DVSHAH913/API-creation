// Enhancements inside Program.cs - replace CRUD methods from Activity 1 with these updated ones
app.MapPost("/users", (User user, List<User> users) =>
{
    if (string.IsNullOrEmpty(user.Name) || !user.Email.Contains("@"))
        return Results.BadRequest("Invalid user data.");
    users.Add(user);
    return Results.Created($"/users/{user.Id}", user);
});

app.MapGet("/users/{id}", (int id, List<User> users) =>
{
    try
    {
        var user = users.FirstOrDefault(u => u.Id == id);
        return user != null ? Results.Ok(user) : Results.NotFound();
    }
    catch
    {
        return Results.Problem("An error occurred retrieving the user.");
    }
});

// Also optimize GET all endpoint
app.MapGet("/users", (List<User> users) =>
{
    var orderedUsers = users.OrderBy(u => u.Id).ToList(); // Example optimization
    return Results.Ok(orderedUsers);
});
