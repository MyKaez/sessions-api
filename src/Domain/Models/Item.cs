using System.Text.Json;

namespace Domain.Models;

public record Item
{
    public Guid Id { get; init; }

    public Guid ControlId { get; init; }

    public JsonElement Configuration { get; init; }
}