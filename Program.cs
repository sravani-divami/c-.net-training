
// // using Microsoft.EntityFrameworkCore;
// // using MyFirstWebApiProj.Data;

// var builder = WebApplication.CreateBuilder(args);

// // Add services to the container.
// // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
// builder.Services.AddOpenApi();

// //adding dbcontext service - need to remove later


// // builder.Services.AddDbContext<StudentDbContext>(options =>
// //     options.UseNpgsql(
// //         builder.Configuration.GetConnectionString("DefaultConnection")
// //     ));

// var app = builder.Build();

// //need to remove this code later
// // using (var scope = app.Services.CreateScope())
// // {
// //     var context = scope.ServiceProvider.GetRequiredService<
// //         MyFirstWebApiProj.Data.StudentDbContext>();

// //     var canConnect = context.Database.CanConnect();
// //     Console.WriteLine($"DB Connected: {canConnect}");
// // }

// // Configure the HTTP request pipeline.
// if (app.Environment.IsDevelopment())
// {
//     app.MapOpenApi();
// }

// app.UseHttpsRedirection();

// var summaries = new[]
// {
//     "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
// };

// app.MapGet("/weatherforecast", () =>
// {
//     var forecast =  Enumerable.Range(1, 5).Select(index =>
//         new WeatherForecast
//         (
//             DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
//             Random.Shared.Next(-20, 55),
//             summaries[Random.Shared.Next(summaries.Length)]
//         ))
//         .ToArray();
//     return forecast;
// })
// .WithName("GetWeatherForecast");

// app.Run();

// record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
// {
//     public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
// }

using Microsoft.EntityFrameworkCore;
using MyFirstWebApiProj.Data;
using MyFirstWebApiProj.Repositories;
using MyFirstWebApiProj.Services;

var builder = WebApplication.CreateBuilder(args);

// Register DbContext
builder.Services.AddDbContext<StudentDbContext>(options =>
    options.UseNpgsql(
        builder.Configuration.GetConnectionString("DefaultConnection")
    ));

// Register Repository
builder.Services.AddScoped<IStudentRepository, StudentRepository>();

// Register Service
builder.Services.AddScoped<IStudentService, StudentService>();

// Add Controllers
builder.Services.AddControllers();

// Swagger / OpenAPI
builder.Services.AddOpenApi();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();


app.UseHttpsRedirection();

app.MapControllers();
 app.UseSwagger();
app.UseSwaggerUI();

app.Run();
