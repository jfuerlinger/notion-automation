namespace NotionAutomation.Core
{
  public interface INotionService
  {
    Task<InvoiceDto> GetInvoiceByIdAsync(Guid invoiceId);
  }
}