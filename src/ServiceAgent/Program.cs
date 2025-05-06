using Microsoft.AspNetCore.Mvc;
using Scalar.AspNetCore;
using ServiceAgent;
using ServiceAgent.Demo;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddHostedService<ServiceAgentWorker>();
builder.Services.AddSingleton<IServiceAgent<RoomServiceAgentContext, RoomServiceAgentParameter>, ServiceAgentWorker>();
builder.Services.AddScoped<RoomService>();
builder.Services.AddScoped<GameService>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}

app.UseHttpsRedirection();

app.MapGet("/rooms", ([FromServices] IServiceAgent<RoomServiceAgentContext, RoomServiceAgentParameter> roomServiceAgentContext) =>
    {
        var contexts = roomServiceAgentContext.GetContexts();
        return Results.Ok(contexts);
    })
    .WithName("GetAllRooms");

app.MapGet("/rooms/{roomId}", (string roomId, [FromServices] IServiceAgent<RoomServiceAgentContext, RoomServiceAgentParameter> roomServiceAgentContext) =>
    {
        var roomServiceAgent = roomServiceAgentContext.GetContext(roomId);
        if (roomServiceAgent is null)
        {
            return Results.NotFound($"Room {roomId} not found");
        }

        return Results.Ok(roomServiceAgent);
    })
    .WithName("GetRoom");

app.MapPost("/rooms/{roomId}", (string roomId, [FromServices] IServiceAgent<RoomServiceAgentContext, RoomServiceAgentParameter> roomServiceAgentContext) =>
    {
        var roomServiceAgent = roomServiceAgentContext.GetContext(roomId);
        if (roomServiceAgent is not null)
        {
            return Results.Conflict($"Room {roomId} already exists");
        }

        roomServiceAgentContext.StartAsync(roomId, new RoomServiceAgentParameter
        {
            Name = "Room " + roomId,
            Description = "this is a test room",
            Capacity = 1,
            GameType = "test"
        }).Forget();
        roomServiceAgent = roomServiceAgentContext.GetContext(roomId);

        return Results.Ok(roomServiceAgent);
    })
    .WithName("CreateRoom");

app.MapDelete("/rooms/{roomId}", async (string roomId, [FromServices] IServiceAgent<RoomServiceAgentContext, RoomServiceAgentParameter> roomServiceAgentContext) =>
    {
        var roomServiceAgent = roomServiceAgentContext.GetContext(roomId);
        if (roomServiceAgent is null)
        {
            return Results.BadRequest($"Room {roomId} not found");
        }

        await roomServiceAgentContext.StopAsync(roomId);

        return Results.Ok(roomId);
    })
    .WithName("DeleteRoom");

app.MapPost("/rooms/{roomId}/games", async (string roomId, 
        [FromServices] IServiceAgent<RoomServiceAgentContext, RoomServiceAgentParameter> roomServiceAgentContext,
        [FromBody] (string id, string name, string type) game,
        CancellationToken cancellation) =>
    {
        var roomServiceAgent = roomServiceAgentContext.GetContext(roomId);
        if (roomServiceAgent is  null)
        {
            return Results.BadRequest($"Room {roomId} not found");
        }

        await roomServiceAgent.Room.StartGameAsync(game.id, game.name, game.type, cancellation);

        return Results.Ok(roomServiceAgent);
    })
    .WithName("StartGame");

app.Run();