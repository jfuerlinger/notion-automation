namespace NotionAutomation.Core.Model
{

  public class InternalPatientResult
  {
    public string _object { get; set; }
    public string id { get; set; }
    public DateTime created_time { get; set; }
    public DateTime last_edited_time { get; set; }
    public Created_By created_by { get; set; }
    public Last_Edited_By last_edited_by { get; set; }
    public Cover cover { get; set; }
    public Icon icon { get; set; }
    public Parent parent { get; set; }
    public bool archived { get; set; }
    public Properties properties { get; set; }
    public string url { get; set; }
  }
  
  public class Cover
  {
    public string type { get; set; }
    public External external { get; set; }
  }

  public class External
  {
    public string url { get; set; }
  }

  public class Icon
  {
    public string type { get; set; }
    public string emoji { get; set; }
  }

  public class Properties
  {
    public Svnr SVNr { get; set; }
    public Vorname Vorname { get; set; }
    public Nachname Nachname { get; set; }
    public Arztrechnungen Arztrechnungen { get; set; }
    public Tags Tags { get; set; }
    public DoctorsName Name { get; set; }
  }

  public class Svnr
  {
    public string id { get; set; }
    public string type { get; set; }
    public Rich_Text[] rich_text { get; set; }
  }


  public class Vorname
  {
    public string id { get; set; }
    public string type { get; set; }
    public Rich_Text[] rich_text { get; set; }
  }


  public class Nachname
  {
    public string id { get; set; }
    public string type { get; set; }
    public Rich_Text[] rich_text { get; set; }
  }

 
  public class Arztrechnungen
  {
    public string id { get; set; }
    public string type { get; set; }
    public Relation[] relation { get; set; }
    public bool has_more { get; set; }
  }


  public class DoctorsTitle
  {
    public string type { get; set; }
    public Text text { get; set; }
    public Annotations annotations { get; set; }
    public string plain_text { get; set; }
    public object href { get; set; }
  }
}
