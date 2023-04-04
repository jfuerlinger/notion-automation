namespace NotionAutomation.Core
{
  public record Document(string FileName, byte[] Content);
  public record InvoiceDto(string Nr, string? Patient, DateTime Behandlungsdatum, Document[] BelegeEinreichung, Document[] BelegeOeGK, string? Arzt);
  

  public interface INotionService
  {
    Task<InvoiceDto> GetInvoiceByIdAsync(Guid invoiceId);
    Task<Document> GetInvoiceAsZipAsync(Guid invoiceId);
  }
}