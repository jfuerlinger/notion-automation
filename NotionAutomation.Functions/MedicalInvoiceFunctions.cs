
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using NotionAutomation.Core;
using System.Net;
using System.Text;

namespace NotionAutomation.Functions
{
  public class MedicalInvoiceFunctions
  {
    private readonly ILogger _logger;
    private readonly IConfiguration _configuration;
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly INotionService _notionService;

    public MedicalInvoiceFunctions(
      IConfiguration configuration,
      ILoggerFactory loggerFactory,
      IHttpClientFactory httpClientFactory,
      INotionService notionService)
    {
      _logger = loggerFactory.CreateLogger<MedicalInvoiceFunctions>();
      _configuration = configuration;
      _httpClientFactory = httpClientFactory;
      _notionService = notionService;
    }
    [Function("DownloadDemo")]
    public HttpResponseData RunDownloadDemo([HttpTrigger(AuthorizationLevel.Function, "get", Route = "download")] HttpRequestData req)
    {
      _logger.LogInformation("C# HTTP trigger was called.");

      
      var response = req.CreateResponse();
      response.StatusCode = HttpStatusCode.OK;
      
      response.Body = GenerateStreamFromString("Das ist ein Test!\nNoch eine Zeile.");
      
      response.Headers.Add("Content-Disposition", "attachment; filename=sample.txt");
      response.Headers.Add("Content-Type", "application/octet-stream");

      return response;
    }

    [Function("ButtonDemo")]
    public HttpResponseData RunButtonDemo([HttpTrigger(AuthorizationLevel.Function, "get", Route = "button")] HttpRequestData req)
    {
      _logger.LogInformation("C# HTTP trigger was called.");


      var response = req.CreateResponse();
      response.StatusCode = HttpStatusCode.OK;
      response.Headers.Add("Content-Type", "text/html");
      response.Body = GenerateStreamFromString(
        """
        <!DOCTYPE html>
        <html lang="en">
        <head>
            <meta charset="UTF-8">
            <meta http-equiv="X-UA-Compatible" content="IE=edge">
            <meta name="viewport" content="width=device-width, initial-scale=1.0">
            <title>Button</title>

        </head>
        <body>
            <a id="btnDownload" href="#">🚀 Download</a>

            <script>
                onReady = function() {
                    console.log("onReady was called!");
                    const btnDownload = document.getElementById("btnDownload");
                    if(btnDownload) {
                        const notionSiteUrl = window.parent.location.search; //document.referrer;
                        const newUrl = `https://wq3k0xvq-7234.euw.devtunnels.ms/api/download${encodeURIComponent(notionSiteUrl)}`;
                        console.log(`Set url to '${newUrl}'`);
                        btnDownload.href = newUrl;
                    }
                }();
            </script>
        </body>
        </html>
        """);

      return response;
    }

    public static Stream GenerateStreamFromString(string s)
    {
      var stream = new MemoryStream();
      var writer = new StreamWriter(stream);
      writer.Write(s);
      writer.Flush();
      stream.Position = 0;
      return stream;
    }

    [Function("DownloadInvoice")]
    public async Task<HttpResponseData> RunDownloadInvoiceAsync([HttpTrigger(AuthorizationLevel.Function, "get", Route ="download-invoice")] HttpRequestData req)
    {
      _logger.LogInformation("C# HTTP trigger was called.");

      //string? content = await req.ReadAsStringAsync(Encoding.UTF8);
      //ArgumentException.ThrowIfNullOrEmpty(content);

      string? url = req.Query["url"];
      ArgumentException.ThrowIfNullOrEmpty(url);

      // https://www.notion.so/jfuerlinger/2023-00909-2fa6a0aceed646d59a8cffea433cbe52?pvs=4
      var processor = new NotionUriProcessor(new Uri(url));

      Guid pageId = processor.GetPageId();

      //using var notionClient = _httpClientFactory.CreateClient("notionClient");


      var zipDocument = await _notionService.GetInvoiceAsZipByIdAsync(pageId.ToString());

      var response = req.CreateResponse();
      response.StatusCode = HttpStatusCode.OK;

      response.Body = new MemoryStream(zipDocument.Content);

      response.Headers.Add("Content-Disposition", $"attachment; filename={zipDocument.FileName}");
      response.Headers.Add("Content-Type", "application/zip");

      return response;


    }
  }
}
