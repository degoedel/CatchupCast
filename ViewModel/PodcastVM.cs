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
using System.Windows.Input;
using Microsoft.Practices.Prism.Commands;
using PodCatchup.Events;

namespace PodCatchup.ViewModel
{
  public class PodcastVM : BindableBase
  {
    #region Members
    private Podcast _podcast;
    ObservableEpisodesCollection _episodes;
    #endregion

    #region Constructors
    public PodcastVM(IUnityContainer container)
    {
      Container = container;
      _podcast = new Podcast();
      _episodes = new ObservableEpisodesCollection();
      ContextReadCommand = new DelegateCommand<object>(this.OnContextRead, this.CanContextRead);
      ContextNewCommand = new DelegateCommand<object>(this.OnContextNew, this.CanContextNew);
      updateEpisodeCollection();
    }

    #endregion

    #region Properties
    private IUnityContainer Container { get; set; }

    public Podcast Podcast
    {
      get { return _podcast; }
      set 
      { 
        _podcast = value;
        updateEpisodeCollection();
      }
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

    public ObservableEpisodesCollection Episodes
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
    public ICommand ContextReadCommand { get; set; }
    public ICommand ContextNewCommand { get; set; }
    #endregion

    #region Interactivity
    private void OnContextRead(object arg)
    {
      EpisodeVM current = (EpisodeVM)arg;
      current.State = EpisodeVM.EpisodeState.Done;
    }

    private bool CanContextRead(object arg)
    {
      return true;
    }

    private void OnContextNew(object arg)
    {
      EpisodeVM current = (EpisodeVM)arg;
      current.State = EpisodeVM.EpisodeState.New;
    }

    private bool CanContextNew(object arg)
    {
      return true;
    }
    #endregion

    public void RefreshEpisodes()
    {
      ISyndicationAnalyzer analyzer = Container.Resolve<ISyndicationAnalyzer>();
      analyzer.RefreshPodcast(ref _podcast);
      updateEpisodeCollection();

    }

    private void updateEpisodeCollection()
    {
      _episodes.Clear();
      foreach(Episode ep in _podcast.Episodes)
      {
        EpisodeVM evm = Container.Resolve<EpisodeVM>();
        evm.Episode = ep;
        Episodes.Add(evm);
      }
      Episodes.Sort("Published", System.ComponentModel.ListSortDirection.Descending);
    }
  }
}
