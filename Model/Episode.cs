using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PodCatchup.Model
{
  public class Episode
  {
    public DateTime LastPlayed { get; set; }
    public UInt64 Signet { get; set; }
    public String Title { get; set; }
    public String Subtitle { get; set; }
    public String Url { get; set; }
    public String Duration { get; set; }
    public String Summary { get; set; }
    public String Guid { get; set; }
    public DateTime Published { get; set; }
    public String Cover { get; set; }
  }
}
