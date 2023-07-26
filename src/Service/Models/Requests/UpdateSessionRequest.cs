using System.Text.Json;

namespace Service.Models.Requests;

public record UpdateSessionRequest
{
    public Guid ControlId { get; set; }
    public JsonElement Configuration { get; set; }
}