namespace Infrastructure.Services;

public interface IConnectionService
{
    Task<bool> CreateSession(Guid sessionId, string connectionId);
    
    Task<bool> CreateSessionItem(Guid sessionId, Guid itemId, string connectionId);
}