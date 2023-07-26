using System.Text.Json;

namespace Service.Models.Requests;

public record UserRequest
{
    public required JsonElement Configuration { get; init; }
}