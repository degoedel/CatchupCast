using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatchupCast.Model
{
  public class PodcastLibrary
  {
    public Dictionary<String, Podcast> Library { get; set; }

    public PodcastLibrary()
    {
        Library = new Dictionary<String, Podcast>();
    }
  }
}
