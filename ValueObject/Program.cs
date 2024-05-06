using Api.Abstractions;
using Api.Behaviors;
using Api.ChannelHostedService;
using Api.Commands.Products;
using Api.Extensions;
using Api.Processors;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence.Context;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("Database")));

builder.Services.AddMediatR(config =>
{
    config.RegisterServicesFromAssembly(typeof(Program).Assembly);

    config.AddOpenBehavior(typeof(LoggingBehavior<,>));
});

builder.Services.AddSingleton<ITasksQueue, TasksQueue>();
builder.Services.AddScoped<WorkItemProcessor, TestWorkItemProcessor>();
builder.Services.AddScoped<WorkItemProcessor, FailOperationProcessor>();
builder.Services.AddScoped<IBackgroundProcessorsFactory, BackgroundProcessorsFactory>();
builder.Services.AddHostedService<WorkItemsHostedService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    //app.ApplyMigrations();
}

app.MapPost("api/products", async (ISender sender) =>
{
    var productId = await sender.Send(new CreateProduct.Command());

    return Results.Ok(productId);
});

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

public partial class Program { }