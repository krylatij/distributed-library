using DistributedLibrary.Services.Services;

namespace DistributedLibrary.UI.Data;

public class WeatherForecastService
{
    private readonly LibraryService _libraryService;
    private readonly ILogger<WeatherForecastService> _logger;

    public WeatherForecastService(LibraryService libraryService, ILogger<WeatherForecastService> logger)
    {
        _libraryService = libraryService;
        _logger = logger;
    }

    private static readonly string[] Summaries = new[]
    {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

    public async Task<WeatherForecast[]> GetForecastAsync(DateTime startDate)
    {
        _logger.LogInformation("starting get books");

        var books = await _libraryService.GetBooksAsync();

        return Enumerable.Range(1, 5).Select(index => new WeatherForecast
        {
            Date = startDate.AddDays(index),
            TemperatureC = Random.Shared.Next(-20, 55),
            Summary = Summaries[Random.Shared.Next(Summaries.Length)]
        }).ToArray();
    }
}