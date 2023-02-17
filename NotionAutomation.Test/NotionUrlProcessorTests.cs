using NotionAutomation.Core;

namespace NotionAutomation.Test
{
  public class NotionUrlProcessorTests
  {
    [Fact]
    public void GetPageId_CallWithValidLongUrl_GetCorrectPageId()
    {
      INotionUriProcessor sut = new NotionUriProcessor(new Uri("https://www.notion.so/jfuerlinger/2023-00909-2fa6a0aceed646d59a8cffea433cbe52?pvs=4"));

      Guid pageId = sut.GetPageId();

      Assert.Equal(Guid.Parse("2fa6a0aceed646d59a8cffea433cbe52"), pageId);
    }

    [Fact]
    public void GetPageId_CallWithValidUrl_GetCorrectPageId()
    {
      INotionUriProcessor sut = new NotionUriProcessor(new Uri("https://www.notion.so/jfuerlinger/2fa6a0aceed646d59a8cffea433cbe52"));

      Guid pageId = sut.GetPageId();

      Assert.Equal(Guid.Parse("2fa6a0aceed646d59a8cffea433cbe52"), pageId);
    }

    [Fact]
    public void GetPageId_CallWithInValidUrl_GetCorrectPageId()
    {
      INotionUriProcessor sut = new NotionUriProcessor(new Uri("https://www.notion.so/jfuerlinger/xxx2fa6a0aceed646d59a8cffea433cbe52?pvs=4"));

      Assert.Throws<InvalidOperationException>(() => sut.GetPageId());
    }
  }
}