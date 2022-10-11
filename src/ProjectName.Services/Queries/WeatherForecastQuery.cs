using ProjectName.Common.Models;
using ProjectName.Infrastructure.Query;

namespace ProjectName.Services.Queries
{
    public class WeatherForecastQuery : IQuery<List<WeatherForecast>>
    {
        public string? City { get; set; }
    }

    public class WeatherForecastQueryHandler : IQueryHandler<WeatherForecastQuery, List<WeatherForecast>>
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        public Task<List<WeatherForecast>> Execute(WeatherForecastQuery query, CancellationToken cancellationToken = default)
        {
            return Task.FromResult(Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                City = query.City,
                Date = DateTime.Now.AddDays(index),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            }).ToList());
        }
    }
}
