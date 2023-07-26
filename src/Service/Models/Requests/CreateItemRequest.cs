using System.Text.Json;

namespace Service.Models.Requests;

public record CreateItemRequest
{
    public required JsonElement Configuration { get; init; }
}