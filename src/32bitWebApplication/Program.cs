using Google.Apis.Requests.Parameters;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

var summaries = new[]
{
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};

app.MapGet("/weatherforecast", () =>
    {
        var forecast = Enumerable.Range(1, 5).Select(index =>
                new WeatherForecast
                (
                    DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                    Random.Shared.Next(-20, 55),
                    summaries[Random.Shared.Next(summaries.Length)]
                ))
            .ToArray();


        var dic = new Dictionary<string, object>
        {
            { "test", "TWVldCBCYXNlNjQgRGVjb2RlIGFuZCBFbmNvZGUsIGEgc2ltcGxlIG9ubGluZSB0b29sIHRoYXQgZG9lcyBleGFjdGx5IHdoYXQgaXQgc2F5czogZGVjb2RlcyBmcm9tIEJhc2U2NCBlbmNvZGluZyBhcyB3ZWxsIGFzIGVuY29kZXMgaW50byBpdCBxdWlja2x5IGFuZCBlYXNpbHkuIEJhc2U2NCBlbmNvZGUgeW91ciBkYXRhIHdpdGhvdXQgaGFzc2xlcyBvciBkZWNvZGUgaXQgaW50byBhIGh1bWFuLXJlYWRhYmxlIGZvcm1hdC4K" },
            { "hoge", "1" },
            { "fuga", "1" },
        };
        var collection = ParameterCollection.FromDictionary(dic);
        foreach (var c in collection)
        {
            Console.WriteLine(c);
        }
        return forecast;
    })
    .WithName("GetWeatherForecast");

app.Run();

record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}