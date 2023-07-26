namespace Service.Models.Responses;

public record SessionControlDto : SessionDto
{
    public required string ControlId { get; init; }
}