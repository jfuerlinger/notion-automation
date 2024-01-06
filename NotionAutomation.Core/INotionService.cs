using NotionAutomation.Core.Model;

namespace NotionAutomation.Core
{
  public record Document(string FileName, byte[] Content);
  public record InvoiceDto(string Nr, string? Patient, DateTime Behandlungsdatum, Document[] BelegeEinreichung, Document[] BelegeOeGK, string? Arzt);
  

  public interface INotionService
  {
    Task<InvoiceInfoDto[]> GetAllInvoicesAsync();
    Task<InvoiceDto> GetInvoiceByIdAsync(string invoiceId);
    Task<Document> GetInvoiceAsZipByIdAsync(string invoiceId);
  }
}