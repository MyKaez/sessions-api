namespace Infrastructure.Database;

public class Session
{
    public Guid Id { get; set; }
    
    public required string ControlId { get; set; }

    public string? Configuration { get; set; }
    
    public DateTime ExpiresAt { get; set; }
    
    public DateTime Created { get; set; }

    public DateTime Updated { get; set; }

    public ICollection<SessionItem> Items { get; set; } = null!;
    
    public Connection Connection { get; set; }
}