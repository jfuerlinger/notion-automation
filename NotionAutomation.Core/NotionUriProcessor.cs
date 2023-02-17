namespace NotionAutomation.Core
{
  public class NotionUriProcessor : INotionUriProcessor
  {
    private readonly Uri _notionUri;

    public NotionUriProcessor(Uri notionUri)
    {
      _notionUri = notionUri;
    }

    public Guid GetPageId()
    {
      //https://www.notion.so/jfuerlinger/2023-00909-2fa6a0aceed646d59a8cffea433cbe52?pvs=4

      string lastSegment = _notionUri.Segments.Last();
      int sepeartor = lastSegment.LastIndexOf('-');

      if (Guid.TryParse(sepeartor != -1 ? lastSegment.Substring(sepeartor + 1) : lastSegment, out Guid pageId))
      {
        return pageId;
      }
      else
      {
        throw new InvalidOperationException("The notionUri is not in a valid format!");
      }
    }
  }
}