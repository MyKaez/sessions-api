using Infrastructure.Database;

namespace Infrastructure.Repositories;

public interface IItemRepository
{
    ValueTask<SessionItem?> GetById(Guid itemId, CancellationToken cancellationToken);

    Task Create(Guid sessionId, SessionItem item, CancellationToken cancellationToken);
    
    Task<SessionItem?> Update(Guid itemId, Action<SessionItem> update, CancellationToken cancellationToken);
    
    Task<SessionItem[]> GetBySessionId(Guid sessionId, CancellationToken cancellationToken);
}