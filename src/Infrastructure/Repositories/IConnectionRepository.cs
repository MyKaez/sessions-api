using Infrastructure.Database;

namespace Infrastructure.Repositories;

public interface IConnectionRepository
{
    Task<ICollection<SessionItem>> GetAll();

    Task Add(string connectionId, Guid sessionId);

    Task Update(string connectionId, Guid userId);

    Task Remove(string connectionId);

    Task<SessionItem?> Get(string connectionId);
}