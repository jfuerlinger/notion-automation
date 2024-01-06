using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotionAutomation.Core.Model
{
  public class Created_By
  {
    public string _object { get; set; }
    public string id { get; set; }
  }

  public class Last_Edited_By
  {
    public string _object { get; set; }
    public string id { get; set; }
  }

  public class Parent
  {
    public string type { get; set; }
    public string database_id { get; set; }
  }

  public class Text
  {
    public string content { get; set; }
    public object link { get; set; }
  }

  public class Rich_Text
  {
    public string type { get; set; }
    public Text text { get; set; }
    public Annotations annotations { get; set; }
    public string plain_text { get; set; }
    public object href { get; set; }
  }

  public class Annotations
  {
    public bool bold { get; set; }
    public bool italic { get; set; }
    public bool strikethrough { get; set; }
    public bool underline { get; set; }
    public bool code { get; set; }
    public string color { get; set; }
  }

  public class Tags
  {
    public string id { get; set; }
    public string type { get; set; }
    public object[] multi_select { get; set; }
  }

  public class ID
  {
    public string id { get; set; }
    public string type { get; set; }
    public Unique_Id unique_id { get; set; }
  }

  public class Unique_Id
  {
    public string prefix { get; set; }
    public int number { get; set; }
  }


}
