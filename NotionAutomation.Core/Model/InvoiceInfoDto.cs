namespace NotionAutomation.Core.Model
{

  public class InvoiceInfoRootobject
  {
    public string _object { get; set; }
    public Result[] results { get; set; }
    public object next_cursor { get; set; }
    public bool has_more { get; set; }
    public string type { get; set; }
    public Page_Or_Database page_or_database { get; set; }
    public string developer_survey { get; set; }
    public string request_id { get; set; }
  }

  public class Page_Or_Database
  {
  }

  public class Result
  {
    public string _object { get; set; }
    public string id { get; set; }
    public DateTime created_time { get; set; }
    public DateTime last_edited_time { get; set; }
    public Created_By created_by { get; set; }
    public Last_Edited_By last_edited_by { get; set; }
    public object cover { get; set; }
    public object icon { get; set; }
    public Parent parent { get; set; }
    public bool archived { get; set; }
    public InvoiceInfoDto properties { get; set; }
    public string url { get; set; }
    public object public_url { get; set; }
  }

  

  public class InvoiceInfoDto
  {
    public EingereichtMerkur EingereichtMerkur { get; set; }
    public BelegÖGK BelegÖGK { get; set; }
    public Kommentar Kommentar { get; set; }
    public ErstattungSumme ErstattungSumme { get; set; }
    public EingereichtÖGK EingereichtÖGK { get; set; }
    public Patient Patient { get; set; }
    public BestätigungMerkur BestätigungMerkur { get; set; }
    public ID ID { get; set; }
    public Betrag Betrag { get; set; }
    public Belege Belege { get; set; }
    public ErstattungÖGK ErstattungÖGK { get; set; }
    public ErstattungMerkur ErstattungMerkur { get; set; }
    public Behandlungsdatum Behandlungsdatum { get; set; }
    public Ärzte Ärzte { get; set; }
    public Status Status { get; set; }
    public Tags Tags { get; set; }
    public BestätigungÖGK BestätigungÖGK { get; set; }
    public Nr Nr { get; set; }
  }

}
