using Microsoft.AspNetCore.Mvc;
using ProjectName.Common.Models;
using ProjectName.Infrastructure.Mediator;
using ProjectName.Services.Queries;

namespace ProjectName.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private readonly IMediator _mediator;

        public WeatherForecastController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet(Name = "GetWeatherForecast")]
        public async Task<ActionResult<List<WeatherForecast>>> Get(string city, CancellationToken cancellationToken)
        {
            return Ok(await _mediator.Send(new WeatherForecastQuery { City = city }, cancellationToken));
        }
    }
}