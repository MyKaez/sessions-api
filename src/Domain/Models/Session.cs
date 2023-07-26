using System.Text.Json;

namespace Domain.Models;

public record Session
{
    private readonly List<Item> _users = new();

    public required Guid Id { get; init; }

    public Guid? ControlId { get; init; }

    public required string Name { get; init; }

    public required string Status { get; init; }

    public DateTime Updated { get; set; }
    
    public DateTime ExpiresAt { get; init; }

    public JsonElement? Configuration { get; init; }

    public IEnumerable<Item> Users => _users;
    
    public DateTime? StartTime { get; set; }
    
    public DateTime? EndTime { get; set; }

    public Session Add(Item item)
    {
        _users.Add(item);
        return this;
    }
}