using System.Text.Json;
using Application.Services;
using Domain.Models;
using Infrastructure.Repositories;

namespace Infrastructure.Services;

public class SessionService:ISessionService
{
    private readonly ISessionRepository _sessionRepository;

    public SessionService(ISessionRepository sessionRepository)
    {
        _sessionRepository = sessionRepository;
    }

    public Task<Session?> GetById(Guid id, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<Session>> GetAll(CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<Session?> CreateSession(string name, JsonElement? configuration, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task DeleteUser(Guid sessionId, Guid userId, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task DeleteSession(Guid sessionId, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public async Task<bool> Update(Session session, JsonElement configuration, CancellationToken cancellationToken)
    {
        var now = DateTime.UtcNow;
        var update = await _sessionRepository.Update(session.Id, s =>
        {
            s.Updated = now;
            s.ExpiresAt = now.AddMinutes(30);
            s.Configuration = configuration.ToString();
        }, cancellationToken);

        return update?.Updated == now;
    }
}