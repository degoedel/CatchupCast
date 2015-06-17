using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatchupCast.Model
{
  public class Podcast
  {
    public Podcast()
    {
      Episodes = new List<Episode>();
    }

    public String Syndication { get; set; }
    public List<Episode> Episodes { get; set; }
    public String Cover { get; set; }
    public String Title { get; set; }
    public String Summary { get; set; }
  }
}
