using System.Text.Json;

namespace Service.Models.Responses;

public record ItemDto
{
    public Guid Id { get; init; }

    public JsonElement Configuration { get; init; }
}