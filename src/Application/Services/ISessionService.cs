using System.Text.Json;
using Domain.Models;

namespace Application.Services;

public interface ISessionService
{
    Task<Session?> GetById(Guid id, CancellationToken cancellationToken);

    Task<IEnumerable<Session>> GetAll(CancellationToken cancellationToken);

    Task<Session?> CreateSession(JsonElement? configuration, CancellationToken cancellationToken);

    Task DeleteUser(Guid sessionId, Guid userId, CancellationToken cancellationToken);
    
    Task DeleteSession(Guid sessionId, CancellationToken cancellationToken);
    
    Task<bool> Update(Session session, Action<Session> update, CancellationToken cancellationToken);
}