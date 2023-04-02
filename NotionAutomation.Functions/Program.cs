using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Net.Http.Headers;

var builder = new HostBuilder()
    .ConfigureFunctionsWorkerDefaults();



builder.ConfigureServices((hostContext, services) =>
  services.AddHttpClient("notionClient", (httpClient) =>
  {
    httpClient.BaseAddress = new Uri("https://api.notion.com");
    httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {Environment.GetEnvironmentVariable("NotionApiKey")}");
    httpClient.DefaultRequestHeaders.Add("Notion-Version", "2022-06-28");
    httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
  }));

var host = builder.Build();


host.Run();
