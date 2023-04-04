using Microsoft.Extensions.Configuration;
using NotionAutomation.Core;
using System.Diagnostics;
using System.Net.Http.Headers;

//Guid INVOICEID = Guid.Parse("2fa6a0aceed646d59a8cffea433cbe52");
Guid INVOICEID = Guid.Parse("d9b1ffb7c72f43f1830006c29070cbfc");



static IConfiguration LoadConfiguration()
{
  var config = new ConfigurationBuilder()
     .AddJsonFile("local.settings.json")
      .AddEnvironmentVariables()
      .Build();

  return config;
}

var config = LoadConfiguration();

using HttpClient notionHttpClient = new();
notionHttpClient.BaseAddress = new Uri("https://api.notion.com");
notionHttpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {config["NotionApiKey"]}");
notionHttpClient.DefaultRequestHeaders.Add("Notion-Version", "2022-06-28");
notionHttpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

using HttpClient plainHttpClient = new();

INotionService notionService = new NotionService(notionHttpClient, plainHttpClient);

string input;
do
{
  Console.WriteLine("Fetch data ...");
  var sw = Stopwatch.StartNew();
  //var invoice = await notionService.GetInvoiceByIdAsync(INVOICEID);
  var zipFile = await notionService.GetInvoiceAsZipAsync(INVOICEID);
  sw.Stop();

  //Console.WriteLine($"Rechnung='{invoice.Nr}' Patient='{invoice.Patient}' Arzt='{invoice.Arzt}', Belege-Einreichung='{invoice.BelegeEinreichung.Length}', Belege-ÖGK='{invoice.BelegeOeGK.Length}'");

  await File.WriteAllBytesAsync(@$"c:\temp\notion-automation\{zipFile.FileName}", zipFile.Content);

  Console.WriteLine($"Data retrieval took {sw.ElapsedMilliseconds} msec");

  Console.WriteLine("'E' to Exit - other key to continue");
  input = Console.ReadLine()!;
}
while (input.ToUpper() != "E");