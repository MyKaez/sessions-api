namespace Service.Models.Responses;

public record ItemControlDto : ItemDto
{
    public Guid ControlId { get; init; }
}