using Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories.Database;

public class ItemRepository : IItemRepository
{
    private readonly DatabaseContext _context;

    public ItemRepository(DatabaseContext context)
    {
        _context = context;
    }

    public ValueTask<SessionItem?> GetById(Guid itemId, CancellationToken cancellationToken)
    {
        return _context.FindAsync<SessionItem>(new object[] { itemId }, cancellationToken);
    }

    public async Task Create(Guid sessionId, SessionItem item, CancellationToken cancellationToken)
    {
        await _context.AddAsync(item, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task<SessionItem?> Update(Guid userId, Action<SessionItem> update, CancellationToken cancellationToken)
    {
        var session = await GetById(userId, cancellationToken);

        if (session is null)
            return null;

        update(session);
        _context.Update(session);

        await _context.SaveChangesAsync(cancellationToken);

        return session;
    }

    public async Task<SessionItem[]> GetBySessionId(Guid sessionId, CancellationToken cancellationToken)
    {
        var items =
            from item in _context.SessionItems
            where item.SessionId == sessionId
            where !item.IsDeleted
            select item;

        return await items.ToArrayAsync(cancellationToken);
    }
}