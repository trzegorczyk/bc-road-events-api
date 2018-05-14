using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace bc_road_events_api.Controllers
{
    [Produces("application/json")]
    [Route("api/Weather")]
    public class WeatherController : Controller
    {
        // GET api/values/5
        [HttpGet("{latitude},{longitude}")]
        public async Task<IActionResult> Get(decimal latitude, decimal longitude, string exclude)
        {
            using (var client = new HttpClient())
            {
                try
                {
                    client.BaseAddress = new Uri("https://api.darksky.net");
                    var response = await client.GetAsync($"/forecast/42e5323109f1a54d62b4ac8a9d4e197a/{latitude},{longitude}?exclude={exclude}&units=ca");
                    response.EnsureSuccessStatusCode();
                    var stringResult = await response.Content.ReadAsStringAsync();
                    return Ok(Newtonsoft.Json.Linq.JObject.Parse(stringResult));
                }
                catch (HttpRequestException httpRequestException)
                {
                    return BadRequest($"Error getting weather from OpenWeather: {httpRequestException.Message}");
                }
            }

        }
    }
}