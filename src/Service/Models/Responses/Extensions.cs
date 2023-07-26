namespace Service.Models.Responses;

public static class Extensions
{
    public static ListDto<T> ToListDto<T>(this IReadOnlyCollection<T> items)
    {
        return new(items);
    }
}