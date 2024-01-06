using NotionAutomation.Core.Model;
using System.IO.Compression;
using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;

namespace NotionAutomation.Core
{
  public sealed class NotionService : INotionService
  {
    private const string _databaseId = "8c96ab39483f4573a941d97067c6ae01";

    private readonly HttpClient _notionHttpClient;
    private readonly HttpClient _plainHttpClient;

    public NotionService(HttpClient notionHttpClient, HttpClient plainHttpClient)
    {
      _notionHttpClient = notionHttpClient;
      _plainHttpClient = plainHttpClient;
    }

    public async Task<InvoiceDto> GetInvoiceByIdAsync(string invoiceId)
    {
      InternalInvoiceResult? invoice =
        await _notionHttpClient.GetFromJsonAsync<InternalInvoiceResult>($"v1/pages/{invoiceId:N}")
          ?? throw new Exception($"Unable to retrieve invoice with id '{invoiceId}'");

      string nr = invoice.properties.Nr.title.Single().plain_text;
      DateTime behandlungsdatum = DateTime.Parse(invoice.properties.Behandlungsdatum.date.start);
      var fetchPatientTask = GetPatientsNameAsync(invoice);
      var fetchArztTask = GetDoctorsNameAsync(invoice);
      var fetchBelegeTask = GetBelegeAsync(invoice);
      var fetchBelegeOegkTask = GetBelegeOegkAsync(invoice);

      await Task.WhenAll(new[] { fetchPatientTask, fetchArztTask });
      await Task.WhenAll(new[] { fetchBelegeTask, fetchBelegeOegkTask });

      return new InvoiceDto(
        Nr: nr,
        Behandlungsdatum: behandlungsdatum,
        Patient: fetchPatientTask.Result,
        BelegeEinreichung: fetchBelegeTask.Result,
        BelegeOeGK: fetchBelegeOegkTask.Result,
        Arzt: fetchArztTask.Result);
    }

    private async Task<Document[]> GetBelegeOegkAsync(InternalInvoiceResult invoice)
    {
      var files = invoice.properties.BelegÖGK?.files;

      if (files != null && files.Any())
      {
        return await DownloadDocumentsAsync(invoice.properties.BelegÖGK?.files!, "BelegÖGK");
      }

      return await ValueTask.FromResult(Array.Empty<Document>());
    }

    private async Task<Document[]> GetBelegeAsync(InternalInvoiceResult invoice)
    {
      var files = invoice.properties.Belege?.files;

      if (files != null && files.Any())
      {
        return await DownloadDocumentsAsync(invoice.properties.Belege?.files!, "Einreichung");
      }

      return await ValueTask.FromResult(Array.Empty<Document>());
    }

    private async ValueTask<Document[]> DownloadDocumentsAsync(Model.File[] files, string documentPrefix)
    {
      var urls = files?.Select(f => f.file.url);

      if (urls != null && urls.Any())
      {
        List<Task<Document>> downloadTasks = new();
        int idx = 1;
        foreach (var url in urls)
        {
          string documentName = CalculateDocumentName(documentPrefix, url, urls.Count(), idx);
          downloadTasks.Add(Task.Run(async () => new Document(documentName, await _plainHttpClient.GetByteArrayAsync(url))));
          idx++;
        }

        await Task.WhenAll(downloadTasks);

        return downloadTasks
          .Select(t => t.Result)
          .ToArray();
      }

      return await Task.FromResult(Array.Empty<Document>());
    }

    private static string CalculateDocumentName(string documentPrefix, string url, int countOfDocuments, int idx)
    {
      if (countOfDocuments > 1)
      {
        return $"{documentPrefix}_{idx:00}.{GetFileExtenion(url)}";
      }
      else
      {
        return $"{documentPrefix}.{GetFileExtenion(url)}";
      }
    }

    private static string? GetFileExtenion(string url)
    {
      string? extension = null;
      if (!string.IsNullOrEmpty(url))
      {
        int idxOfPoint = url.LastIndexOf('.');
        if (idxOfPoint != -1)
        {
          int idxOfQuestionMark = url.IndexOf("?");
          if (idxOfQuestionMark != -1)
          {
            return url.Substring(idxOfPoint + 1, idxOfQuestionMark - idxOfPoint - 1);
          }
          else
          {
            return url[(idxOfPoint + 1)..];
          }

        }
      }

      return extension;
    }

    private async Task<string?> GetDoctorsNameAsync(InternalInvoiceResult invoice)
    {
      string? doctorsName = null;
      var doctorsId = invoice.properties.Ärzte?.relation?.FirstOrDefault()?.id;

      if (!string.IsNullOrEmpty(doctorsId))
      {
        Guid id = Guid.Parse(doctorsId);
        var patient = await _notionHttpClient.GetFromJsonAsync<InternalDoctorResult>($"v1/pages/{id:N}");
        doctorsName = patient?.properties?.Name?.title?.FirstOrDefault()?.plain_text;
      }

      return doctorsName;
    }

    private async Task<string?> GetPatientsNameAsync(InternalInvoiceResult invoice)
    {
      string? patientsName = null;
      var patientId = invoice.properties.Patient?.relation?.FirstOrDefault()?.id;

      if (!string.IsNullOrEmpty(patientId))
      {
        Guid id = Guid.Parse(patientId);

        var patient = await _notionHttpClient.GetFromJsonAsync<InternalPatientResult>($"v1/pages/{id:N}");
        patientsName = patient?.properties?.Name?.title?.FirstOrDefault()?.plain_text;
      }

      return patientsName;
    }

    public async Task<Document> GetInvoiceAsZipByIdAsync(string invoiceId)
    {
      var invoice = await GetInvoiceByIdAsync(invoiceId);

      using var memoryStream = new MemoryStream();
      using (var archive = new ZipArchive(memoryStream, ZipArchiveMode.Create, false))
      {
        if (invoice.BelegeEinreichung.Any())
        {
          await AddFilesIntoZipFolderAsync(invoice.BelegeEinreichung, "00_Einreichung", archive);
        }

        if (invoice.BelegeOeGK.Any())
        {
          await AddFilesIntoZipFolderAsync(invoice.BelegeOeGK, "01_ÖGK", archive);
        }

      }

      return new Document($"{invoice.Behandlungsdatum:yyyy-MM-dd}_{NormalizeName(invoice.Patient)}_{NormalizeName(invoice.Arzt)}.zip", memoryStream.ToArray());
    }

    private static async Task AddFilesIntoZipFolderAsync(Document[] documents, string folder, ZipArchive archive)
    {
      archive.CreateEntry($"{folder}/");

      foreach (Document document in documents)
      {
        var fileInZip = archive.CreateEntry($"{folder}/{document.FileName}");

        using Stream stream = fileInZip.Open();
        await stream.WriteAsync(document.Content.AsMemory(0, document.Content.Length));
        await stream.FlushAsync();
      }
    }

    private static string? NormalizeName(string? arzt)
    {
      if (!string.IsNullOrEmpty(arzt))
      {
        arzt = arzt
                .Trim()
                .Replace("Dr.", "")
                .Replace(" ", "");
      }

      return arzt;
    }

    public async Task<InvoiceInfoDto[]> GetAllInvoicesAsync()
    {
      var invoices = new List<InvoiceInfoDto>();


      InvoiceInfoRootobject? result;
      do
      {
        var response= await _notionHttpClient.PostAsync($"v1/databases/{_databaseId}/query", new StringContent("", new MediaTypeHeaderValue("application/json")))
          ?? throw new Exception("Unable to query the invoices!");

        result = await JsonSerializer.DeserializeAsync<InvoiceInfoRootobject>(response.Content.ReadAsStream());

        //invoices.AddRange(result.results.Select(r => r.properties));

      } while (result!.has_more);


      return invoices.ToArray();
    }
  }
}
