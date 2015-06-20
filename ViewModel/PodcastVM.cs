using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PodCatchup.Model;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Prism.Mvvm;
using PodCatchup.Infrastructure;
using System.Collections.ObjectModel;

namespace PodCatchup.ViewModel
{
  public class PodcastVM : BindableBase
  {
    #region Members
    private Podcast _podcast;
    ObservableCollection<EpisodeVM> _episodes;
    #endregion

    #region Constructors
    public PodcastVM(IUnityContainer container)
    {
      Container = container;
      _podcast = new Podcast();
      _episodes = new ObservableCollection<EpisodeVM>();
      updateEpisodeCollection();
    }

    #endregion

    #region Properties
    private IUnityContainer Container { get; set; }

    public Podcast Podcast
    {
      get { return _podcast; }
      set { _podcast = value; }
    }

    public String Syndication
    {
      get { return _podcast.Syndication; }
      set 
      { 
        _podcast.Syndication = value;
        ISyndicationAnalyzer analyzer = Container.Resolve<ISyndicationAnalyzer>();
        analyzer.InitializePodcast(ref _podcast);
        updateEpisodeCollection();
      }
    }

    public String Title
    {
      get { return _podcast.Title; }
      set { _podcast.Title = value; }
    }

    public String Cover
    {
      get { return _podcast.Cover; }
      set { _podcast.Cover = value; }
    }

    public ObservableCollection<EpisodeVM> Episodes
    {
      get { return _episodes; }
      set { SetProperty(ref this._episodes, value); }
    }

    public String Summary
    {
      get { return _podcast.Summary; }
      set { _podcast.Summary = value; }
    }
    #endregion

    #region CommandProperties
    #endregion

    #region Interactivity
    #endregion

    private void updateEpisodeCollection()
    {
      _episodes.Clear();
      foreach(Episode ep in _podcast.Episodes)
      {
        EpisodeVM evm = Container.Resolve<EpisodeVM>();
        evm.Episode = ep;
        Episodes.Add(evm);
      }
    }
  }
}
