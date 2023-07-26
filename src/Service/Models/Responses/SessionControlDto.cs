namespace Service.Models.Responses;

public record SessionControlDto : SessionDto
{
    public Guid ControlId { get; init; }
}