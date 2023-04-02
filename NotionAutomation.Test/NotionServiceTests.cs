using Microsoft.Extensions.Configuration;
using NotionAutomation.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace NotionAutomation.Test
{
  public class NotionServiceTests
  {
    public static IConfiguration LoadConfiguration()
    {
      var config = new ConfigurationBuilder()
         .AddJsonFile("local.settings.json")
          .AddEnvironmentVariables()
          .Build();
      
      return config;
    }

    [Fact]
    public void Configuration_LoadNotionApiKey_ShouldNotBeNull()
    {
      var config = LoadConfiguration();

      var notionApiKey = config["Values:NotionApiKey"];

      Assert.NotNull(notionApiKey);
    }

    [Fact]
    public async Task GetInvoiceByIdAsync_CallWithValidInvoiceId_GetCorrectData()
    {
      var config = LoadConfiguration();

      using HttpClient httpClient = new();
      httpClient.BaseAddress = new Uri("https://api.notion.com");
      httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {config["Values:NotionApiKey"]}");
      httpClient.DefaultRequestHeaders.Add("Notion-Version", "2022-06-28");
      httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
      
      INotionService notionService = new NotionService(httpClient);

      var invoice = await notionService.GetInvoiceByIdAsync(new Guid("2fa6a0aceed646d59a8cffea433cbe52"));

      Assert.NotNull(invoice);
      Assert.Equal("2023/00909", invoice.Nr);

    }
  }
}
