using System.Reflection.Metadata;

namespace IAsyncEnumerable;

public record NumbersResponse(List<int> Numbers, string? NextPageToken);

public static class NumberClients
{
    public const int NumbersInStock = 100;
    public const int MaxPageSize = 10;

    public static async Task<NumbersResponse> ListNumbersAsync(int pageSize = 10, string? pageToken = default)
    {
        Task.Delay(500).Wait();

        pageSize = Math.Min(pageSize, MaxPageSize);
        var start = pageToken == null ? 0 : int.Parse(pageToken);
        var end = start + pageSize is var e && e < NumbersInStock ? e : NumbersInStock;

        return new NumbersResponse(Enumerable.Range(start, end - start).ToList(),
            end < NumbersInStock ? end.ToString() : null);

    }

    public static NumbersResponse ListNumbers(int pageSize = 10, string? pageToken = default)
    {
        Task.Delay(500).Wait();

        pageSize = Math.Min(pageSize, MaxPageSize);
        var start = pageToken == null ? 0 : int.Parse(pageToken);
        var end = start + pageSize is var e && e < NumbersInStock ? e : NumbersInStock;

        return new NumbersResponse(Enumerable.Range(start, end - start).ToList(),
            end < NumbersInStock ? end.ToString() : null);

    }
}