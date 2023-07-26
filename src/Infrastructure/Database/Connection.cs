namespace Infrastructure.Database;

public class Connection
{
    public Guid Id { get; set; }
    
    public required string ConnectionId { get; set; }

    public Guid? SessionId { get; set; }

    public Session? Session { get; set; }

    public Guid? SessionItemId { get; set; }

    public SessionItem? SessionItem { get; set; }
}