using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Threading.Tasks;

namespace SimpleApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RiotController : ControllerBase
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public RiotController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        [HttpGet("summoner")]
        public async Task<IActionResult> GetSummonerInfo()
        {
            try
            {
                var requestUrl = "https://europe.api.riotgames.com/riot/account/v1/accounts/by-riot-id/lulufizz/euw?api_key=RGAPI-cb139c63-5909-4352-834c-f6e6cc84db03";
                var httpClient = _httpClientFactory.CreateClient();

                var response = await httpClient.GetAsync(requestUrl);

                if (!response.IsSuccessStatusCode)
                {
                    return BadRequest($"Error fetching summoner info: {response.ReasonPhrase}");
                }

                var content = await response.Content.ReadAsStringAsync();
                HttpContext.Response.Headers.Add("Access-Control-Allow-Origin", "http://localhost:3000");
                return Ok(content);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
