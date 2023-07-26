using System.Text.Json;
using Domain.Models;

namespace Application.Services;

public interface IItemService
{
    Task<Item?> GetById(Guid userId, CancellationToken cancellationToken);

    Task<Item> Create(Session session, JsonElement configuration, CancellationToken cancellationToken);

    Task<Item[]> GetBySessionId(Guid sessionId, CancellationToken cancellationToken);
}