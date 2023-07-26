using System.Text.Json;

namespace Service.Models.Requests;

public record SessionRequest
{
    public JsonElement? Configuration { get; init; }
}