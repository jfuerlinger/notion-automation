using System.Net;
using System.Net.Http.Json;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using NotionAutomation.Core;
using NotionAutomation.Core.Model;

namespace NotionAutomation.Functions
{
  public class MedicalInvoiceFunctions
  {
    private readonly ILogger _logger;
    private readonly IConfiguration _configuration;
    private readonly IHttpClientFactory _httpClientFactory;

    public MedicalInvoiceFunctions(
      IConfiguration configuration,
      ILoggerFactory loggerFactory,
      IHttpClientFactory httpClientFactory)
    {
      _logger = loggerFactory.CreateLogger<MedicalInvoiceFunctions>();
      _configuration = configuration;
      _httpClientFactory = httpClientFactory;
    }
    [Function("DownloadDemo")]
    public IActionResult RunDownloadDemo([HttpTrigger(AuthorizationLevel.Function, "get", "download-demo")] HttpRequestData req)
    {
      _logger.LogInformation("C# HTTP trigger was called.");

      byte[] filebytes = Encoding.UTF8.GetBytes("Das ist der Inhalt der Textdatei");

      return new FileContentResult(filebytes, "application/octet-stream")
      {
        FileDownloadName = "Sample.txt"
      };
    }


    [Function("GenerateOeGKContent")]
    public async Task<HttpResponseData> RunAsync([HttpTrigger(AuthorizationLevel.Function, "get")] HttpRequestData req)
    {
      _logger.LogInformation("C# HTTP trigger was called.");

      string? content = await req.ReadAsStringAsync(Encoding.UTF8);
      ArgumentException.ThrowIfNullOrEmpty(content);

      // https://www.notion.so/jfuerlinger/2023-00909-2fa6a0aceed646d59a8cffea433cbe52?pvs=4
      var processor = new NotionUriProcessor(new Uri(content));

      Guid pageId = processor.GetPageId();

      using var notionClient = _httpClientFactory.CreateClient("notionClient");

      var response = req.CreateResponse(HttpStatusCode.OK);
      response.Headers.Add("Content-Type", "text/plain; charset=utf-8");

      response.WriteString("Welcome to Azure Functions!");

      return response;
    }
  }
}
