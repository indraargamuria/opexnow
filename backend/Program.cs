using Microsoft.EntityFrameworkCore;
using backend.Infrastructure.Database;
using backend.Models;
var builder = WebApplication.CreateBuilder(args);

// // // ✅ Add this
// // builder.Services.AddCors(options =>
// // {
// //     options.AddPolicy("AllowAll",
// //         policy => policy.AllowAnyOrigin()
// //                         .AllowAnyMethod()
// //                         .AllowAnyHeader());
// // });

// Add services to the container.
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("Default")));

var app = builder.Build();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

var summaries = new[]
{
    "Freezing", "Bracing", "Chilly", "Cool", "Mild",
    "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};

// app.UseCors("AllowAll");
app.MapGet("/todos", async (AppDbContext db) =>
{
    return await db.TodoItems.ToListAsync();
});

app.MapPost("/todos", async (AppDbContext db, TodoItem todo) =>
{
    db.TodoItems.Add(todo);
    await db.SaveChangesAsync();
    return Results.Ok(todo);
});
app.MapGet("/weatherforecast", () =>
{
    var forecast = Enumerable.Range(1, 5).Select(index =>
        new WeatherForecast(
            DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            Random.Shared.Next(-20, 55),
            summaries[Random.Shared.Next(summaries.Length)]
        )
    ).ToArray();

    return forecast;
})
.WithName("GetWeatherForecast");

app.Run();

record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}