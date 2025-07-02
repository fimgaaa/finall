
using Microsoft.EntityFrameworkCore;
using MauiToDoFinal.Api.Data;
using System.Net.Mail;
using MauiToDoFinal.Api.Models;
using MauiToDoFinal.Api.Services;





var builder = WebApplication.CreateBuilder(args);

// Swagger ve EF Core servislerini ekle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<IUserService, UserService>();

var smtpHost = builder.Configuration["Smtp:Host"] ?? "localhost";
var smtpPort = builder.Configuration.GetValue<int>("Smtp:Port", 25);


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// test endpointi
app.MapGet("/", () => "MauiToDoFinal API çalışıyor!");

// Tüm ToDo verilerini getir
app.MapGet("/api/todo", async (AppDbContext db) =>
    await db.ToDoItems.ToListAsync());

// Yeni ToDo ekle
app.MapPost("/api/todo", async (ToDoItem item, AppDbContext db) =>
{
    item.CreatedAt = DateTime.Now;
    db.ToDoItems.Add(item);
    await db.SaveChangesAsync();
    return Results.Created($"/api/todo/{item.Id}", item);
});

// Tek bir ToDo getir
app.MapGet("/api/todo/{id}", async (int id, AppDbContext db) =>
    await db.ToDoItems.FindAsync(id) is ToDoItem item ? Results.Ok(item) : Results.NotFound());

// ToDo güncelle
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

//  ToDo sil
app.MapDelete("/api/todo/{id}", async (int id, AppDbContext db) =>
{
    var todo = await db.ToDoItems.FindAsync(id);
    if (todo == null) return Results.NotFound();

    db.ToDoItems.Remove(todo);
    await db.SaveChangesAsync();

    return Results.Ok();
});

// Görevleri giriş yapan kullanıcıya göre ve önceliğe göre sıralı getir
app.MapGet("/api/todo/user/{username}", async (string username, AppDbContext db) =>
    await db.ToDoItems
            .Where(t => t.CreatedBy == username)
            .OrderByDescending(t => t.Priority)
            .ToListAsync());



// Kullanıcı kaydı

app.MapPost("/api/users/register", async (User user, IUserService service) =>
{
    var created = await service.RegisterUserAsync(user);
    return created != null
        ? Results.Ok(created)
        : Results.BadRequest("Bu kullanıcı adı veya e-posta zaten kayıtlı.");

});

app.MapPost("/api/users/login", async (User loginUser, IUserService service) =>
{

    var user = await service.LoginUserAsync(loginUser);
    return user != null ? Results.Ok(user) : Results.Unauthorized();
});


app.MapPost("/api/users/forgot-password", async (ForgotPasswordRequest req, IUserService service) =>
{

    var result = await service.SendForgotPasswordEmailAsync(req.Email);
    return result ? Results.Ok("E-posta gönderildi") : Results.NotFound("Kullanıcı bulunamadı");
});

app.Run();
