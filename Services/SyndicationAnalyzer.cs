using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PodCatchup.Infrastructure;
using PodCatchup.Model;
using System.Xml.Linq;

namespace PodCatchup.Services
{
  public class SyndicationAnalyzer : ISyndicationAnalyzer
  {
    #region ISyndicationAnalyzer Members
    public void InitializePodcast(ref Podcast podcast)
    {
      var xml = XDocument.Load(podcast.Syndication);
      XNamespace ns = "http://www.itunes.com/dtds/podcast-1.0.dtd";
      podcast.Title = xml.Element("rss").Element("channel").Element("title").Value;
      XElement coverelem = xml.Element("rss").Element("channel").Element("image");
      if (coverelem == null)
      {
        podcast.Cover = xml.Element("rss").Element("channel").Element(ns + "image").Attribute("href").Value;
      }
      else
      {
        podcast.Cover = coverelem.Element("url").Value;
      }
      var summaryElement = xml.Element("rss").Element("channel").Element(ns + "summary");
      if (summaryElement == null)
      {
        summaryElement = xml.Element("rss").Element("channel").Element(ns + "subtitle");
      }
      if (summaryElement == null)
      {
        summaryElement = xml.Element("rss").Element("channel").Element("description");
      }
      if (summaryElement != null)
      {
        podcast.Summary = summaryElement.Value;
      }

      loadItemsFromFeed(ref podcast, ref xml, ref ns);
    }

    public void RefreshPodcast(ref Podcast podcast)
    {
      var xml = XDocument.Load(podcast.Syndication);
      XNamespace ns = "http://www.itunes.com/dtds/podcast-1.0.dtd";
      loadItemsFromFeed(ref podcast, ref xml, ref ns);
    }
    #endregion

    private void loadItemsFromFeed(ref Podcast podcast, ref XDocument xml, ref XNamespace ns)
    {
      foreach (var item in xml.Descendants("item"))
      {
        Episode nep = new Episode();
        nep.Title = item.Element("title").Value;
        nep.Summary = item.Element(ns + "summary").Value;
        nep.Subtitle = item.Element(ns + "subtitle").Value;
        nep.Url = item.Element("enclosure").Attribute("url").Value;
        if (item.Element(ns + "duration") != null)
        {
          nep.Duration = item.Element(ns + "duration").Value;
        }
        nep.Guid = item.Element("guid").Value;
        nep.Published = DateTime.Parse(item.Element("pubDate").Value);
        if (item.Element(ns + "image") != null)
        {
          nep.Cover = item.Element(ns + "image").Attribute("href").Value;
        }
        else
        {
          nep.Cover = podcast.Cover;
        }
        if (podcast.Episodes.FindIndex(ep => ep.Guid == nep.Guid) < 0)
        {
          podcast.Episodes.Add(nep);
        }
      }
    }

  }
}
