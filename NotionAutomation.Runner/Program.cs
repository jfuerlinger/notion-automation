using Microsoft.Extensions.Configuration;
using NotionAutomation.Core;
using System.Diagnostics;
using System.Net.Http.Headers;

//Guid INVOICEID = Guid.Parse("2fa6a0aceed646d59a8cffea433cbe52");
//string invoiceId = "d9b1ffb7c72f43f1830006c29070cbfc";
string invoiceId = "RG-35";


INotionService notionService = BuildNotionService();

string input;
do
{
  var sw = Stopwatch.StartNew();

  //await DownloadInvoiceAsync(invoiceId, notionService);
  await ListAllInvoices(notionService);

  sw.Stop();
  Console.WriteLine($"-> {sw.ElapsedMilliseconds} msec");

  Console.WriteLine("'E' to Exit - other key to continue");
  input = Console.ReadLine()!;
} while (input.ToUpper() != "E");

static async Task ListAllInvoices(INotionService notionService)
{
  var invoices = await notionService.GetAllInvoicesAsync();

  foreach (var invoice in invoices)
  {
        await Console.Out.WriteLineAsync($"{invoice.Patient}\t{invoice.Behandlungsdatum.date}");
    }
}

static async Task DownloadInvoiceAsync(string invoiceId, INotionService notionService)
{
  Console.WriteLine("Fetch data ...");
  //var invoice = await notionService.GetInvoiceByIdAsync(INVOICEID);
  var zipFile = await notionService.GetInvoiceAsZipByIdAsync(invoiceId);

  //Console.WriteLine($"Rechnung='{invoice.Nr}' Patient='{invoice.Patient}' Arzt='{invoice.Arzt}', Belege-Einreichung='{invoice.BelegeEinreichung.Length}', Belege-ÖGK='{invoice.BelegeOeGK.Length}'");

  await File.WriteAllBytesAsync(@$"c:\temp\notion-automation\{zipFile.FileName}", zipFile.Content);

}

static INotionService BuildNotionService()
{
  var config = new ConfigurationBuilder()
     .AddJsonFile("local.settings.json")
      .AddEnvironmentVariables()
      .Build();


  HttpClient notionHttpClient = new();
  notionHttpClient.BaseAddress = new Uri("https://api.notion.com");
  notionHttpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {config["NotionApiKey"]}");
  notionHttpClient.DefaultRequestHeaders.Add("Notion-Version", "2022-06-28");
  notionHttpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
  
  return  new NotionService(notionHttpClient, new HttpClient());
}