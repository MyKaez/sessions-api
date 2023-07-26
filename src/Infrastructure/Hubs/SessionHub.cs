using Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Logging;
using Polly;

namespace Infrastructure.Hubs;

// https://code-maze.com/netcore-signalr-angular-realtime-charts/
// https://guidnew.com/en/blog/signalr-modules-with-a-shared-connection-using-a-csharp-source-generator/

// https://code-maze.com/how-to-send-client-specific-messages-using-signalr/
// https://learn.microsoft.com/en-us/aspnet/signalr/overview/guide-to-the-api/working-with-groups
public class SessionHub : Hub
{
    private readonly ILogger<SessionHub> _logger;

    private readonly AsyncPolicy _retryPolicy = Policy
        .Handle<SqlException>(e =>
            e.Message.Contains("deadlocked on lock resources with another process"))
        .WaitAndRetryAsync(5, current => TimeSpan.FromMilliseconds(50 * (current + 1)));

    public SessionHub(ILogger<SessionHub> logger)
    {
        _logger = logger;
    }

    public override Task OnConnectedAsync()
    {
        _logger.LogInformation("Initiated connection {Connection}", Context.ConnectionId);

        return base.OnConnectedAsync();
    }

    public async Task<bool> RegisterSession(Guid sessionId, [FromServices] IConnectionService connectionService)
    {
        await Groups.AddToGroupAsync(Context.ConnectionId, sessionId.ToString());
        return await connectionService.CreateSession(sessionId, Context.ConnectionId);
    }
    
    public async Task<bool> RegisterItem(Guid sessionId, Guid itemId, [FromServices] IConnectionService connectionService)
    {
        await Groups.AddToGroupAsync(Context.ConnectionId, sessionId.ToString());
        return await connectionService.CreateSessionItem(sessionId, itemId, Context.ConnectionId);
    }
    //
    // public override async Task OnDisconnectedAsync(Exception? exception)
    // {
    //     if (exception is not null)
    //         _logger.LogError(exception, "Erroneously disconnected connection {Connection}", Context.ConnectionId);
    //
    //     await _retryPolicy.ExecuteAsync(async () =>
    //     {
    //         var connectionService = _serviceProvider.GetRequiredService<IConnectionService>();
    //         var connection = await connectionService.Get(Context.ConnectionId);
    //
    //         if (connection is null)
    //         {
    //             _logger.LogInformation("Disconnected connection {Connection}", Context.ConnectionId);
    //         }
    //         else
    //         {
    //             _logger.LogInformation("Session {SessionID} disconnected from {Connection}",
    //                 connection.SessionId, Context.ConnectionId);
    //
    //             if (connection.UserId.HasValue)
    //                 await DisconnectUser(connection.SessionId, connection.UserId.Value);
    //             else
    //                 await connectionService.Remove(Context.ConnectionId);
    //         }
    //     });
    // }
    
    public Task Alive(Guid? userId)
    {
        if (userId.HasValue)
            _logger.LogInformation("User {UserID} is alive", userId);
        else
            _logger.LogInformation("Host is alive");
        
        return Task.CompletedTask;
    }
}