using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CatchupCast.Infrastructure;
using CatchupCast.Model;
using System.ServiceModel.Syndication;
using System.Xml;

namespace CatchupCast.Services
{
  public class SyndicationAnalyzer : ISyndicationAnalyzer
  {
    #region ISyndicationAnalyzer Members
    public void InitializePodcast(ref Podcast podcast)
    {
      SyndicationFeed feed = SyndicationFeed.Load(XmlReader.Create(podcast.Syndication));
      podcast.Title = feed.Title.Text;
      podcast.Cover = feed.ImageUrl.AbsoluteUri;
      loadItemsFromFeed(ref podcast, ref feed);
    }

    public void RefreshPodcast(ref Podcast podcast)
    {
      SyndicationFeed feed = SyndicationFeed.Load(XmlReader.Create(podcast.Syndication));
      loadItemsFromFeed(ref podcast, ref feed);
    }
    #endregion

    private void loadItemsFromFeed(ref Podcast podcast, ref SyndicationFeed feed)
    {

    }

  }
}
