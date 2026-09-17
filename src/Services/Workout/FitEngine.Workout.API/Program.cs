using FitEngine.Workout.Application.Common.Interfaces;
using FitEngine.Workout.Application.Workouts.Commands.CreateWorkout;
using FitEngine.Workout.Infrastructure;
using FitEngine.Workout.Infrastructure.Persistence;
using FitEngine.Workout.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System.Text.Json.Serialization;



var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var cs = builder.Configuration.GetConnectionString("WorkoutDb");
builder.Services.AddDbContext<WorkoutDbContext>(options =>
    options.UseMySql(cs, new MySqlServerVersion(new Version(8, 0, 36)), b => b.MigrationsAssembly("FitEngine.Workout.Infrastructure")));

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
    });
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();   

builder.Services.AddScoped<IWorkoutRepository, WorkoutRepository>();
builder.Services.AddMediatR(cfg =>
    cfg.RegisterServicesFromAssembly(typeof(CreateWorkoutCommand).Assembly));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
   
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    options.SwaggerEndpoint("/swagger/v1/swagger.json", "FitEngine.Workout.API v1"));
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();


app.Run();


