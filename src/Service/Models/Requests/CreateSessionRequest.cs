using System.Text.Json;

namespace Service.Models.Requests;

public record CreateSessionRequest
{
    public JsonElement? Configuration { get; init; }
}