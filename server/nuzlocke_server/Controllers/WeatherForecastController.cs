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
    public async Task<IActionResult> GetSummonerInfo(string name, string tagline)
    {
        try
        {
            var riotApiKey = _configuration["RiotApi:ApiKey"];
            var httpClient = _httpClientFactory.CreateClient();
            var response = await httpClient.GetAsync($"https://na1.api.riotgames.com/lol/summoner/v4/summoners/by-name/{name}?tagline={tagline}&api_key={riotApiKey}");

            if (!response.IsSuccessStatusCode)
            {
                return BadRequest($"Error fetching summoner info: {response.ReasonPhrase}");
            }

            var content = await response.Content.ReadAsStringAsync();
            var summonerInfo = JsonConvert.DeserializeObject(content);
            return Ok(summonerInfo);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }
}
