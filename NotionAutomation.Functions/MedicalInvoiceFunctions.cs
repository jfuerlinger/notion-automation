using System.Net;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;

namespace NotionAutomation.Functions
{
  public class MedicalInvoiceFunctions
  {
    private readonly ILogger _logger;

    public MedicalInvoiceFunctions(ILoggerFactory loggerFactory)
    {
      _logger = loggerFactory.CreateLogger<MedicalInvoiceFunctions>();
    }

    [Function("GenerateOeGKContent")]
    public HttpResponseData Run([HttpTrigger(AuthorizationLevel.Function, "get")] HttpRequestData req)
    {
      _logger.LogInformation("C# HTTP trigger function processed a request.");

      //https://api.notion.com/v1/pages/2fa6a0aceed646d59a8cffea433cbe52

      // https://www.notion.so/jfuerlinger/2023-00909-2fa6a0aceed646d59a8cffea433cbe52?pvs=4

      var response = req.CreateResponse(HttpStatusCode.OK);
      response.Headers.Add("Content-Type", "text/plain; charset=utf-8");

      response.WriteString("Welcome to Azure Functions!");

      return response;
    }
  }
}
