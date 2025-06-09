using Microsoft.EntityFrameworkCore;
using Persistence;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddDbContext<AppDbContext>(opt =>
{
  opt.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection"));
});
builder.Services.AddCors();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseCors(options => options.AllowAnyHeader().AllowAnyMethod()
  .WithOrigins("http://localhost:3000", "https://localhost:3000")); // Allow any header, any method, and specify the origin
  
app.MapControllers();

using var scope = app.Services.CreateScope(); // when this function goes out of scope (run) anything we use inside of here will be disposed of (using) disposes
var services = scope.ServiceProvider;

try
{
  var context = services.GetRequiredService<AppDbContext>();
  await context.Database.MigrateAsync(); // this will create the database if it does not exist
  await DbInitializer.SeedData(context); // this will seed the database with initial data
}
catch (Exception ex)
{
  var logger = services.GetRequiredService<ILogger<Program>>();
  logger.LogError(ex, "An error occurred during migration");
}

app.Run();
