using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CatchupCast.Model;

namespace CatchupCast.Infrastructure
{
  public interface ISyndicationAnalyzer
  {
    void InitializePodcast(ref Podcast podcast);
    void RefreshPodcast(ref Podcast podcast);
  }
}
