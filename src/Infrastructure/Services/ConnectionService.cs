using Infrastructure.Database;

namespace Infrastructure.Services;

public class ConnectionService : IConnectionService
{
    private DatabaseContext _context;

    public ConnectionService(DatabaseContext context)
    {
        _context = context;
    }

    public async Task<bool> CreateSession(Guid sessionId, string connectionId)
    {
        var con = new Connection
        {
            SessionId = sessionId,
            ConnectionId = connectionId
        };

        await _context.AddAsync(con);
        return await _context.SaveChangesAsync() != 0;
    }

    public async Task<bool> CreateSessionItem(Guid sessionId, Guid itemId, string connectionId)
    {
        var con = new Connection
        {
            SessionId = sessionId,
            SessionItemId = itemId,
            ConnectionId = connectionId
        };

        await _context.AddAsync(con);
        return await _context.SaveChangesAsync() != 0;
    }
}