using System.Reflection;
using ToDo_API.Data;
using FluentValidation;
using FluentValidation.AspNetCore;
using System.Reflection;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<UserRepository>();
builder.Services.AddScoped<CategoryRepository>();
builder.Services.AddScoped<ReminderRepository>();
builder.Services.AddScoped<TaskRepository>();


// Register FluentValidation (Scan the Validators folder dynamically)
builder.Services.AddFluentValidationAutoValidation(); // Enables automatic validation
builder.Services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly()); // Registers all validators in the project



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
