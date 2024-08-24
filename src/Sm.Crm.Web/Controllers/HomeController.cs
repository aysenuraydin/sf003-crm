using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;
using Sm.Crm.Application.Common.Models;
using Sm.Crm.Application.Dtos;
using Sm.Crm.Web.Models;
using System.Diagnostics;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Sm.Crm.Web.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly IConfiguration _configuration;

    public HomeController(ILogger<HomeController> logger, IHttpClientFactory httpClientFactory, IConfiguration configuration)
    {
        _logger = logger;
        _httpClientFactory = httpClientFactory;
        _configuration = configuration;
    }

    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }

    public async Task<IActionResult> Titles()
    {
        var httpClient = _httpClientFactory.CreateClient("CrmApi");
        var httpResponseMessage = await httpClient.GetAsync("titles");

        if (httpResponseMessage.IsSuccessStatusCode)
        {
            using var contentStream = await httpResponseMessage.Content.ReadAsStreamAsync();
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
            var titles = await JsonSerializer.DeserializeAsync<PaginatedResultJson>(contentStream, options);
            ViewBag.Titles = titles.Items;
            return View();
        }

        return View();
    }
    public async Task<IActionResult> DovizKurlari()
    {
        var startDate = DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd");
        var endDate = DateTime.Now.ToString("yyyy-MM-dd");

        var httpClient = _httpClientFactory.CreateClient("CanliDovizApi");
        var httpResponseMessage = await httpClient.GetAsync($"?period=OHLC&code=USD&marketId=0&startDate={startDate}T06:00:00&endDate={endDate}T06:00:00");

        if (httpResponseMessage.IsSuccessStatusCode)
        {
            var strData = await httpResponseMessage.Content.ReadAsStringAsync();
            var usdData = strData.Substring(18, 31);
            var usdArr = usdData.Split('|');

            ViewBag.UsdArr = usdArr;

            return View();
        }

        return View();
    }

    public async Task<IActionResult> GithubBranches()
    {
        var httpRequestMessage = new HttpRequestMessage(
            HttpMethod.Get,
            "https://api.github.com/repos/dotnet/AspNetCore.Docs/branches")
        {
            Headers =
            {
                { HeaderNames.Accept, "application/vnd.github.v3+json" },
                { HeaderNames.UserAgent, "HttpRequestsSample" }
            }
        };

        var httpClient = _httpClientFactory.CreateClient();
        var httpResponseMessage = await httpClient.SendAsync(httpRequestMessage);

        if (httpResponseMessage.IsSuccessStatusCode)
        {
            using var contentStream = await httpResponseMessage.Content.ReadAsStreamAsync();

            var branches = await JsonSerializer.DeserializeAsync<IEnumerable<GitHubBranch>>(contentStream);
            ViewBag.Branches = branches;
            return View();
        }

        return View();
    }
}

public class GitHubBranch
{
    [JsonPropertyName("name")]
    public string Name { get; set; }
}

public class PaginatedResultJson
{
    [JsonPropertyName("items")]
    public List<TitleDto>? Items { get; set; }
}