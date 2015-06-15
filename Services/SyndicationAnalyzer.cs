using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CatchupCast.Infrastructure;
using CatchupCast.Model;

namespace CatchupCast.Services
{
  public class SyndicationAnalyzer : ISyndicationAnalyzer
  {
    #region ISyndicationAnalyzer Members

    void ISyndicationAnalyzer.InitializePodcast(ref Podcast podcast)
    {
      throw new NotImplementedException();
    }

    void ISyndicationAnalyzer.RefreshPodcast(ref Podcast podcast)
    {
      throw new NotImplementedException();
    }

    #endregion
  }
}
