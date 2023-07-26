namespace Service.Models.Responses;

public record ItemControlDto : ItemDto
{
    public string ControlId { get; init; }
}