using System.Text.Json;
using Application.Services;
using AutoMapper;
using Infrastructure.Database;
using Infrastructure.Repositories;
using Session = Domain.Models.Session;

namespace Infrastructure.Services;

public class SessionService: ISessionService
{
    private readonly ISessionRepository _sessionRepository;
    private readonly IMapper _mapper;

    public SessionService(ISessionRepository sessionRepository, IMapper mapper)
    {
        _sessionRepository = sessionRepository;
        _mapper = mapper;
    }

    public Task<Session?> GetById(Guid id, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public async Task<IEnumerable<Session>> GetAll(CancellationToken cancellationToken)
    {
        var sessions = await _sessionRepository.GetAll(cancellationToken);
        var res = _mapper.Map<Session[]>(sessions) ?? Array.Empty<Session>();

        return res;
    }

    public async Task<Session?> CreateSession(JsonElement? configuration, CancellationToken cancellationToken)
    {
        var session = new Database.Session
        {
            Id = Guid.NewGuid(),
            ControlId = Guid.NewGuid().ToString(),
            Configuration = configuration?.ToString(),
            ExpiresAt = DateTime.UtcNow.AddMinutes(30),
            Updated = DateTime.UtcNow,
            Created = DateTime.UtcNow
        };
        var res = _mapper.Map<Session>(session);
        
        await _sessionRepository.Add(session, cancellationToken);

        return res;
    }

    public Task DeleteUser(Guid sessionId, Guid userId, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task DeleteSession(Guid sessionId, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<bool> Update(Session session, Action<Session> update, CancellationToken cancellationToken)
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