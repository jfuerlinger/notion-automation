namespace NotionAutomation.Core.Model
{


  public class InternalInvoiceResult
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
    public InvoiceProperties properties { get; set; }
    public string url { get; set; }
  }

  public class InvoiceProperties
  {
    public EingereichtMerkur EingereichtMerkur { get; set; }
    public BelegÖGK BelegÖGK { get; set; }
    public Kommentar Kommentar { get; set; }
    public ErstattungSumme ErstattungSumme { get; set; }
    public EingereichtÖGK EingereichtÖGK { get; set; }
    public Patient Patient { get; set; }
    public BestätigungMerkur BestätigungMerkur { get; set; }
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

  public class EingereichtMerkur
  {
    public string id { get; set; }
    public string type { get; set; }
    public object date { get; set; }
  }

  public class BelegÖGK
  {
    public string id { get; set; }
    public string type { get; set; }
    public object[] files { get; set; }
  }

  public class Kommentar
  {
    public string id { get; set; }
    public string type { get; set; }
    public object[] rich_text { get; set; }
  }

  public class ErstattungSumme
  {
    public string id { get; set; }
    public string type { get; set; }
    public object number { get; set; }
  }

  public class EingereichtÖGK
  {
    public string id { get; set; }
    public string type { get; set; }
    public Date date { get; set; }
  }

  public class Date
  {
    public string start { get; set; }
    public object end { get; set; }
    public object time_zone { get; set; }
  }

  public class Patient
  {
    public string id { get; set; }
    public string type { get; set; }
    public Relation[] relation { get; set; }
    public bool has_more { get; set; }
  }

  public class Relation
  {
    public string id { get; set; }
  }

  public class BestätigungMerkur
  {
    public string id { get; set; }
    public string type { get; set; }
    public object[] files { get; set; }
  }

  public class Betrag
  {
    public string id { get; set; }
    public string type { get; set; }
    public int number { get; set; }
  }

  public class Belege
  {
    public string id { get; set; }
    public string type { get; set; }
    public File[] files { get; set; }
  }

  public class File
  {
    public string name { get; set; }
    public string type { get; set; }
    public File1 file { get; set; }
  }

  public class File1
  {
    public string url { get; set; }
    public DateTime expiry_time { get; set; }
  }

  public class ErstattungÖGK
  {
    public string id { get; set; }
    public string type { get; set; }
    public object number { get; set; }
  }

  public class ErstattungMerkur
  {
    public string id { get; set; }
    public string type { get; set; }
    public object number { get; set; }
  }

  public class Behandlungsdatum
  {
    public string id { get; set; }
    public string type { get; set; }
    public Date1 date { get; set; }
  }

  public class Date1
  {
    public string start { get; set; }
    public object end { get; set; }
    public object time_zone { get; set; }
  }

  public class Ärzte
  {
    public string id { get; set; }
    public string type { get; set; }
    public Relation1[] relation { get; set; }
    public bool has_more { get; set; }
  }

  public class Relation1
  {
    public string id { get; set; }
  }

  public class Status
  {
    public string id { get; set; }
    public string type { get; set; }
    public Status1 status { get; set; }
  }

  public class Status1
  {
    public string id { get; set; }
    public string name { get; set; }
    public string color { get; set; }
  }

  public class Tags
  {
    public string id { get; set; }
    public string type { get; set; }
    public object[] multi_select { get; set; }
  }

  public class BestätigungÖGK
  {
    public string id { get; set; }
    public string type { get; set; }
    public object[] files { get; set; }
  }

  public class Nr
  {
    public string id { get; set; }
    public string type { get; set; }
    public Title[] title { get; set; }
  }

  public class Title
  {
    public string type { get; set; }
    public Text text { get; set; }
    public Annotations annotations { get; set; }
    public string plain_text { get; set; }
    public object href { get; set; }
  }

}
