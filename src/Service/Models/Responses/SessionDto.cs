using System.Text.Json;

namespace Service.Models.Responses;

public record SessionDto
{
    public required Guid Id { get; init; }

    public JsonElement? Configuration { get; init; }
    
    public required DateTime ExpirationTime { get; init; }

    public DateTime? StartTime { get; set; }
    
    public DateTime? EndTime { get; set; }
}