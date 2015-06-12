using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatchupCast.Model
{
  public class Podcast
  {
    public String Syndication { get; set; }
    public List<Episode> Episodes { get; set; }
    public String Cover { get; set; }
    public String Title { get; set; }
  }
}
