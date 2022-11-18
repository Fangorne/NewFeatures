// See https://aka.ms/new-console-template for more information

using IAsyncEnumerable;

static async Task<bool> IsPrimeAsync(int number)
{
    await Task.Delay(1000);
    if (number == 1) return false;
    if (number == 2) return true;
    //if (number == 6737626471) return true;

    if (number % 2 == 0) return false;

    for (int i = 3; i < number; i += 2)
    {
        if (number % i == 0) return false;
    }
    return true;
}

static bool IsPrime(int number)
{
    if (number == 1) return false;
    if (number == 2) return true;
    //if (number == 6737626471) return true;

    if (number % 2 == 0) return false;

    for (int i = 3; i < number; i += 2)
    {
        if (number % i == 0) return false;
    }
    return true;
}

static IEnumerable<int> ListAllNumbers()
{
    string? pageToken = default;
    var numbers = new List<int>();
    do
    {
        var responses = NumberClients.ListNumbers(pageToken: pageToken);
        pageToken = responses.NextPageToken;
        numbers.AddRange(responses.Numbers);
    } while (pageToken != null);

    return numbers;
}

static IEnumerable<int> ListAllNumbersWithYield()
{
    string? pageToken = default;
    var numbers = new List<int>();
    do
    {
        var responses = NumberClients.ListNumbers(pageToken : pageToken);
        pageToken = responses.NextPageToken;
        foreach (var responsesNumber in responses.Numbers)
        {
            yield return responsesNumber;
        }
    } while (pageToken != null);
}


static async Task<IEnumerable<int>> ListAllNumbersWithAsync()
{
    string? pageToken = default;
    var numbers = new List<int>();
    do
    {
        var responses = await NumberClients.ListNumbersAsync(pageToken: pageToken);
        pageToken = responses.NextPageToken;
        numbers.AddRange(responses.Numbers);
    } while (pageToken != null);

    return numbers;
}


static async IAsyncEnumerable<int> ListAllNumbersWithAsyncEnumerable()
{
    string? pageToken = default;
    var numbers = new List<int>();
    do
    {
        var responses = NumberClients.ListNumbers(pageToken: pageToken);
        pageToken = responses.NextPageToken;
        foreach (var responsesNumber in responses.Numbers)
        {
            yield return responsesNumber;
        }
    } while (pageToken != null);
}


var numbers = ListAllNumbers().Where(IsPrime).Take(5);

foreach (var number in numbers)
{
    Console.WriteLine(number);
}

numbers = ListAllNumbersWithYield().Where(IsPrime).Take(5);
foreach (var number in numbers)
{
    Console.WriteLine(number);
}

numbers = (await ListAllNumbersWithAsync()).Where(IsPrime).Take(5);
foreach (var number in numbers)
{
    Console.WriteLine(number);
}

List<int> primes = new List<int>();
await foreach(var number in ListAllNumbersWithAsyncEnumerable())
{
    if (IsPrime(number))
    {
        primes.Add(number);
    }

    if (primes.Count == 5)
        break;
}

Console.WriteLine(string.Join(", ", primes));

// System.Linq.Async
await ListAllNumbersWithAsyncEnumerable().Where(IsPrime).Take(5).ForEachAsync(Console.WriteLine);

await ListAllNumbersWithAsyncEnumerable().WhereAwait(async n => await IsPrimeAsync(n)).Take(5).ForEachAsync(Console.WriteLine);

Console.ReadLine();



