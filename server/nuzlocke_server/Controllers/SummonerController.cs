using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Threading.Tasks;

[ApiController]
[Route("api")]
public class SummonerController : ControllerBase
{
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly IConfiguration _configuration;

    public SummonerController(IHttpClientFactory httpClientFactory, IConfiguration configuration)
    {
        _httpClientFactory = httpClientFactory;
        _configuration = configuration;
    }

    [HttpGet("summoner")]
    public async Task<IActionResult> GetSummonerInfo()
    {
        try
        {
            var summonerName = "TrojahnPower"; // Hardcoded summoner name
            var tagline = "#EUW"; // Hardcoded tagline
            var apiKey = "RGAPI-f8956065-dbc1-4831-b9c9-102cf6b21a73"; // Hardcoded API key
            var httpClient = _httpClientFactory.CreateClient();

            var response = await httpClient.GetAsync($"https://euw1.api.riotgames.com/lol/summoner/v4/summoners/by-name/Trojahn%20Power?api_key=RGAPI-3011088c-0d33-4170-930b-a108cddce085");

            if (!response.IsSuccessStatusCode)
            {
                return BadRequest($"Error fetching summoner info: {response.ReasonPhrase}");
            }

            var content = await response.Content.ReadAsStringAsync();
            
            // Deserialize the JSON response into SummonerInfo
            var summonerInfo = JsonConvert.DeserializeObject<SummonerInfo>(content);

            return Ok(summonerInfo);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }
}
