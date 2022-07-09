using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace DreamTeamAPI.Controllers
{
    [EnableCors("CorsPolicy")]
    [Route("api/[controller]")]
    [ApiController]
    public class DataController : Controller
    {
        [HttpGet("")]
        public async Task<IActionResult> GetData(string priceCode)
        {
            try
            {
                var client = new HttpClient();
                var request = new HttpRequestMessage
                {
                    Method = HttpMethod.Get,
                    RequestUri = new Uri($"https://alpha-vantage.p.rapidapi.com/query?interval=5min&function=TIME_SERIES_INTRADAY&symbol={priceCode}&datatype=json&output_size=compact"),
                    Headers =
                            {
                                { "X-RapidAPI-Key", "9784d23ae6mshab608d9a38fa8e3p1eaac2jsn510ee5d165cc" },
                                { "X-RapidAPI-Host", "alpha-vantage.p.rapidapi.com" },
                            },
                };
                using (var response = await client.SendAsync(request))
                {
                    response.EnsureSuccessStatusCode();
                    var body = await response.Content.ReadAsStringAsync();

                    return Ok(body);
                }

                return Ok();
            }
            catch (System.Exception Ex)
            {
                return BadRequest(Ex.Message);
            }
        }
    }
}
