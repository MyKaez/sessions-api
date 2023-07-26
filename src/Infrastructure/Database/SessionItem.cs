namespace Infrastructure.Database;

public class SessionItem
{
    public Guid Id { get; set; }
    
    public Guid SessionId { get; set; }

    public Session Session { get; set; } = null!;

    public required string ControlId { get; set; }
    
    public required string ConnectionId { get; set; }

    public required string Configuration { get; set; }

    public bool IsDeleted { get; set; }
    
    public DateTime Created { get; set; }

    public DateTime Updated { get; set; }
}