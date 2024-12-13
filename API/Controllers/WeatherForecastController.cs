using Domain.Entities;

using Interfaces;

using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;
        private readonly IDatStudent _datStudent;

        public WeatherForecastController(ILogger<WeatherForecastController> logger,IDatStudent datStudent)
        {
            _logger = logger;
            _datStudent = datStudent;
        }

        [HttpGet(Name = "GetWeatherForecast")]
        public IEnumerable<WeatherForecast> Get()
        {

            Student student=new Student();
            student.Name = "jose";
            student.SurName = "chim";
            student.Date= DateTime.Now;
            student.Genero = 'M';

            _datStudent.DSave(student);

            var rng = _datStudent.DGet();

            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
        }
    }
}
