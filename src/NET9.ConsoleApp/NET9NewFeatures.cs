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
}