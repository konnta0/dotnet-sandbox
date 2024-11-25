using System.Text.Json;

namespace NET9.ConsoleApp;

public class NET9NewFeatures
{
    public void JsonIndentOption()
    {
        var options = new JsonSerializerOptions
        {
            WriteIndented = true,
            IndentCharacter = '\t', // new in .NET 9
            IndentSize = 3 // new in .NET 9
        };

        var json = JsonSerializer.Serialize(new { Value = 1 }, options);
        Console.WriteLine(json);
    }

    public void JsonDefaultWebOption()
    {
        var webJson = JsonSerializer.Serialize(
            new { SomeValue = 42, Hoo = "test" },
            JsonSerializerOptions.Web 
        );

        Console.WriteLine(webJson);
    }

    public void CountBy()
    {
        const string sourceText = """
                                Lorem ipsum dolor sit amet, consectetur adipiscing elit.
                                Sed non risus. Suspendisse lectus tortor, dignissim sit amet, 
                                adipiscing nec, ultricies sed, dolor. Cras elementum ultrices amet diam.
                            """;

        var mostFrequentWord = sourceText
            .Split([' ', '.', ','], StringSplitOptions.RemoveEmptyEntries)
            .Select(static word => word.ToLowerInvariant())
            .CountBy(static word => word)
            .MaxBy(static pair => pair.Value);

        Console.WriteLine(mostFrequentWord.Key); // amet
    }

    public void AggregateBy()
    {
        (string id, int score)[] data =
        [
            ("0", 42),
            ("1", 5),
            ("2", 4),
            ("1", 10),
            ("0", 25),
        ];

        var aggregatedData =
            data.AggregateBy(
                keySelector: static entry => entry.id,
                seed: 0,
                static (totalScore, curr) => totalScore + curr.score
            );

        foreach (var item in aggregatedData)
        {
            Console.WriteLine(item);
        }
    }
}