public class Program
{
    public static async Task Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add configuration
        builder.Configuration.AddJsonFile("appsettings.json");
        builder.Configuration.AddEnvironmentVariables();

        // Add services to the container.
        builder.Services.AddHttpClient();
        builder.Services.AddControllers();

        var app = builder.Build();

        // Make API call during startup
        var httpClientFactory = app.Services.GetRequiredService<IHttpClientFactory>();
        var httpClient = httpClientFactory.CreateClient();
        var riotApiKey = app.Configuration["RiotApi:RGAPI-3011088c-0d33-4170-930b-a108cddce085"];

        try
        {
            var response = await httpClient.GetAsync($"https://euw1.api.riotgames.com/lol/summoner/v4/summoners/by-name/Trojahn%20Power?api_key=RGAPI-3011088c-0d33-4170-930b-a108cddce085");
            var content = await response.Content.ReadAsStringAsync();
            Console.WriteLine(content);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error fetching summoner info during startup: {ex.Message}");
        }

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }

        app.UseRouting();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });

        await app.RunAsync();
    }
}
