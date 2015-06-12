using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CatchupCast.Model;

namespace CatchupCast.ViewModel
{
  public class PodcastVM
  {
    #region Members
    private Podcast _podcast;
    #endregion

    #region Constructors
    #endregion

    #region Properties
    public Podcast Podcast
    {
      get { return _podcast; }
      set { _podcast = value; }
    }

    public String Syndication
    {
      get { return _podcast.Syndication; }
      set { _podcast.Syndication = value; }
    }

    public String Title
    {
      get { return _podcast.Title; }
      set { _podcast.Title = value; }
    }
    #endregion

    #region CommandProperties
    #endregion

    #region Interactivity
    #endregion

  }
}
