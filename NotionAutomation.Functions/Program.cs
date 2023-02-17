using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NotionAutomation.Core;

var builder = new HostBuilder()
    .ConfigureFunctionsWorkerDefaults();

builder.ConfigureServices(configure =>
  configure.AddHttpClient("notionClient", (httpClient) =>
  {
    httpClient.BaseAddress = new Uri("https://api.notion.com");
    httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer ");
  }));

var host = builder.Build();


host.Run();
