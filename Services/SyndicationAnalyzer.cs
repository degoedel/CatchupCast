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
      foreach (SyndicationItem item in feed.Items)
      {
        Episode nep = new Episode();
        nep.Title = item.Title.Text;
        nep.Summary = item.Summary.Text;
/*
        var xml = XDocument.Load("http://thepointjax.com/Podcast/podcast.xml");

 XNamespace ns = "http://www.itunes.com/dtds/podcast-1.0.dtd";
 foreach (var item in xml.Descendants("item"))
 {
     var title = item.Element("title").Value;
     var subtitle = item.Element(ns + "subtitle").Value;
     var author = item.Element(ns + "author").Value;

     PodcastItemsList.Add (new PodcastItem(title, subtitle, author));
 }
        nep.Duration = item.
        String summary = item.Summary.Text;
 * */
      }
    }

  }
}
