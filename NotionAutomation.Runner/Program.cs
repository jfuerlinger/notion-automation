using Microsoft.Extensions.Configuration;
using NotionAutomation.Core;
using System.Net.Http.Headers;

static IConfiguration LoadConfiguration()
{
  var config = new ConfigurationBuilder()
     .AddJsonFile("local.settings.json")
      .AddEnvironmentVariables()
      .Build();

  return config;
}

var config = LoadConfiguration();

using HttpClient httpClient = new();
httpClient.BaseAddress = new Uri("https://api.notion.com");
httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {config["NotionApiKey"]}");
httpClient.DefaultRequestHeaders.Add("Notion-Version", "2022-06-28");
httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

INotionService notionService = new NotionService(httpClient);

var invoice = await notionService.GetInvoiceByIdAsync(new Guid("2fa6a0aceed646d59a8cffea433cbe52"));