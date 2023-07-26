namespace Service.Models.Responses;

public record ListDto<T>(IReadOnlyCollection<T> Data)
{
    public int Count => Data.Count;
}