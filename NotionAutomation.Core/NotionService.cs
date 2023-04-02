using NotionAutomation.Core.Model;
using System.Net.Http.Json;
using System.Text.Json;

namespace NotionAutomation.Core
{
  public record InvoiceDto(string Nr, string? Patient, DateTime Behandlungsdatum, byte[] Beleg1, byte[] Beleg2, byte[] Beleg3, byte[] BelegOeGK, string Arzt);

  public class NotionService : INotionService
  {
    private readonly HttpClient _httpClient;

    public NotionService(HttpClient httpClient)
    {
      _httpClient = httpClient;
    }

    public async Task<InvoiceDto> GetInvoiceByIdAsync(Guid invoiceId)
    {
      var invoice = await _httpClient.GetFromJsonAsync<InternalInvoiceResult>($"v1/pages/{invoiceId:N}");

      if (invoice == null)
      {
        throw new Exception($"Unable to retrieve invoice with id '{invoiceId}'");
      }

      string nr = invoice.properties.Nr.title.Single().plain_text;
      DateTime behandlungsdatum = DateTime.Parse(invoice.properties.Behandlungsdatum.date.start);
      string? patient = await GetPatientsNameAsync(invoice);
      //string? arzt = await GetDoctorsNameAsync(invoice);

      return new InvoiceDto(
        Nr: nr,
        Behandlungsdatum: behandlungsdatum,
        Patient: patient,
        Beleg1: null,
        Beleg2: null,
        Beleg3: null,
        BelegOeGK: null,
        Arzt: arzt);
    }

    private async Task<string?> GetDoctorsNameAsync(InternalInvoiceResult invoice)
    {
      return "n/a";

      //string? doctorsName = null;
      //var doctorsId = invoice.properties.Ärzte.relation.FirstOrDefault() as JsonElement?;

      //if (doctorsId.HasValue)
      //{
      //  Guid id = doctorsId.Value.GetProperty("id").GetGuid();

      //  var patient = await _httpClient.GetFromJsonAsync<InternalDoctorResult>($"v1/pages/{id:N}");

      //  doctorsName = patient?.properties?.Name?.title?.FirstOrDefault()?.plain_text;
      //}

      //return doctorsName;
    }

    private async Task<string?> GetPatientsNameAsync(InternalInvoiceResult invoice)
    {
      return "n/a";

      //string? patientsName = null;
      //var patientId = invoice.properties.Patient.relation.FirstOrDefault() as JsonElement?;

      //if (patientId.HasValue)
      //{
      //  Guid id = patientId.Value.GetProperty("id").GetGuid();

      //  var patient = await _httpClient.GetFromJsonAsync<InternalPatientResult>($"v1/pages/{id:N}");

      //  patientsName = patient?.properties?.Name?.title?.FirstOrDefault()?.plain_text;
      //}

      //return patientsName;
    }
  }
}
