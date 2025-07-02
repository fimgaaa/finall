//var builder = WebApplication.CreateBuilder(args);
//var app = builder.Build();

//app.MapGet("/", () => "Hello World!");

//app.Run();
using Microsoft.EntityFrameworkCore;
using MauiToDoFinal.Api.Data;
using MauiToDoFinal.Api.Models;

var builder = WebApplication.CreateBuilder(args);

// Swagger ve EF Core servislerini ekle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// ✅ Basit test endpointi
app.MapGet("/", () => "MauiToDoFinal API çalışıyor!");

// ✅ Tüm ToDo verilerini getir
app.MapGet("/api/todo", async (AppDbContext db) =>
    await db.ToDoItems.ToListAsync());

// ✅ Yeni ToDo ekle
app.MapPost("/api/todo", async (ToDoItem item, AppDbContext db) =>
{
    item.CreatedAt = DateTime.Now;
    db.ToDoItems.Add(item);
    await db.SaveChangesAsync();
    return Results.Created($"/api/todo/{item.Id}", item);
});

// ✅ Tek bir ToDo getir
app.MapGet("/api/todo/{id}", async (int id, AppDbContext db) =>
    await db.ToDoItems.FindAsync(id) is ToDoItem item ? Results.Ok(item) : Results.NotFound());

// ✅ ToDo güncelle
app.MapPut("/api/todo/{id}", async (int id, ToDoItem updatedItem, AppDbContext db) =>
{
    var todo = await db.ToDoItems.FindAsync(id);
    if (todo == null) return Results.NotFound();

    todo.Title = updatedItem.Title;
    todo.IsCompleted = updatedItem.IsCompleted;
    todo.UpdatedAt = DateTime.Now;
    todo.UpdatedBy = updatedItem.UpdatedBy;
    await db.SaveChangesAsync();

    return Results.NoContent();
});

// ✅ ToDo sil
app.MapDelete("/api/todo/{id}", async (int id, AppDbContext db) =>
{
    var todo = await db.ToDoItems.FindAsync(id);
    if (todo == null) return Results.NotFound();

    db.ToDoItems.Remove(todo);
    await db.SaveChangesAsync();

    return Results.Ok();
});

// 🔁 Tüm görevleri değil, giriş yapan kullanıcıya göre filtrele
app.MapGet("/api/todo/user/{username}", async (string username, AppDbContext db) =>
    await db.ToDoItems
            .Where(t => t.CreatedBy == username)
            .ToListAsync());

// Kullanıcı kaydı
app.MapPost("/api/users/register", async (User user, AppDbContext db) =>
{
    var exists = await db.Users.AnyAsync(u => u.Username == user.Username);
    if (exists) return Results.BadRequest("Bu kullanıcı adı zaten kayıtlı.");

    db.Users.Add(user);
    await db.SaveChangesAsync();
    return Results.Ok(user);
});
// Kullanıcı girişi
app.MapPost("/api/users/login", async (User loginUser, AppDbContext db) =>
{
    var user = await db.Users
        .FirstOrDefaultAsync(u => u.Username == loginUser.Username && u.Password == loginUser.Password);

    return user != null ? Results.Ok(user) : Results.Unauthorized();
});


app.Run();
