namespace NotionAutomation.Core.Model
{

  public class InternalDoctorResult
  {
    public string _object { get; set; }
    public string id { get; set; }
    public DateTime created_time { get; set; }
    public DateTime last_edited_time { get; set; }
    public Created_By created_by { get; set; }
    public Last_Edited_By last_edited_by { get; set; }
    public object cover { get; set; }
    public Icon icon { get; set; }
    public Parent parent { get; set; }
    public bool archived { get; set; }
    public DoctorsProperties properties { get; set; }
    public string url { get; set; }
  }

  

  public class DoctorsProperties
  {
    public Ort Ort { get; set; }
    public PLZ PLZ { get; set; }
    public Straße Straße { get; set; }
    public Tags Tags { get; set; }
    public DoctorsName Name { get; set; }
  }


  

  public class Ort
  {
    public string id { get; set; }
    public string type { get; set; }
    public Rich_Text[] rich_text { get; set; }
  }

 

  public class PLZ
  {
    public string id { get; set; }
    public string type { get; set; }
    public Rich_Text[] rich_text { get; set; }
  }

  

  public class Straße
  {
    public string id { get; set; }
    public string type { get; set; }
    public Rich_Text[] rich_text { get; set; }
  }
    

  public class DoctorsName
  {
    public string id { get; set; }
    public string type { get; set; }
    public DoctorsTitle[] title { get; set; }
  }

}
