using Microsoft.Practices.Prism.PubSubEvents;
using Microsoft.Practices.Unity;
using PodCatchup.Events;
using PodCatchup.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.Mvvm;
using System.Threading;

namespace PodCatchup.ViewModel
{
  public class PlayerVM : BindableBase
  {
    #region Members
    protected readonly IEventAggregator _eventAggregator;
    private EpisodeVM _episode;
    TimeSpan _currentProgress;
    #endregion

    #region Constructors
    public PlayerVM(IUnityContainer container)
    {
      Container = container;
      _currentProgress = TimeSpan.Parse("00:00:00");
      TogglePlayCommand = new DelegateCommand<object>(this.OnTogglePlay, this.CanTogglePlay);
      _eventAggregator = ApplicationService.Instance.EventAggregator;
      this._eventAggregator.GetEvent<PlayPauseSelectedEpisodeEvent>()
        .Subscribe((episode) => { this.StartOrPauseEpisode(episode); });
      this._eventAggregator.GetEvent<StreamProgressEvent>()
        .Subscribe((signet) => { this.UpdateProgress(signet); }, ThreadOption.UIThread);
      this._eventAggregator.GetEvent<StreamCompletedEvent>()
        .Subscribe((done) => { this.MarkEpisodeComplete(); }, ThreadOption.UIThread);
    }
    #endregion

    #region Properties
    private IUnityContainer Container { get; set; }
    public EpisodeVM Episode
    {
      get { return _episode; }
      set
      {
        SetProperty(ref this._episode, value);
        OnPropertyChanged(() => DurationAsS);
      } 
    }

    private IStreamPlayer StreamPlayer { get; set; }

    public String CurrentPositionAsTimeString
    {
      get
      {
        if (Episode == null)
        {
          return "00:00:00";
        }
        else
        {
          return CurrentProgress.ToString(@"hh\:mm\:ss");
        }
      }
      set { }
    }
    public Double CurrentProgressAsS
    {
      get
      {
        if (Episode == null)
        {
          return 0;
        }
        else
        {
          return CurrentProgress.TotalSeconds;
        }
      }
      set { }
    }

    public Double DurationAsS
    {
      get
      {
        if (Episode == null)
        {
          return 0;
        }
        else
        {
          return TimeSpan.Parse(Episode.Duration).TotalSeconds;
        }
      }
    }

    public TimeSpan CurrentProgress
    {
      get { return _currentProgress; }
      set
      {
        SetProperty(ref this._currentProgress, value);
        Episode.Signet = _currentProgress;
        OnPropertyChanged(() => CurrentPositionAsTimeString);
        OnPropertyChanged(() => CurrentProgressAsS);
      }
    }

    public String ButtonPic
    {
      get
      {
        if (Episode == null)
        {
          return "/PodCatchup;component/icons/Play.png";
        }
        else
        {
          if (Episode.PlayState == EpisodeVM.PlayingState.Playing)
          {
            return "/PodCatchup;component/icons/Pause.png";
          }
          else
          {
            return "/PodCatchup;component/icons/Play.png";
          }
        }
      }
      set
      {

      }
    }
    #endregion

    #region CommandProperties
    public ICommand TogglePlayCommand { get; set; }
    #endregion

    #region Interactivity
    private void StartOrPauseEpisode(EpisodeVM episode)
    {
      if (Episode != null)
      {
        if (Episode.PlayState == EpisodeVM.PlayingState.Playing)
        {
          PauseCurrentEpisode();
          Thread.Sleep(100);
          if (Episode.Url.CompareTo(episode.Url) == 0)
          {
            return;
          }
        }
      }
      Episode = episode;
      if (StreamPlayer == null)
      {
        StreamPlayer = Container.Resolve<IStreamPlayer>();
      }
      PlayCurrentEpisode();
      RaiseCanExecuteChanged();
    }

    public void PauseCurrentEpisode()
    {
      if (StreamPlayer != null)
      {
        StreamPlayer.PauseStream();
      }
      if (Episode != null)
      {
        Episode.PlayState = EpisodeVM.PlayingState.Stopped;
      }
      OnPropertyChanged(() => ButtonPic);
    }

    private void PlayCurrentEpisode()
    {
      StreamPlayer.StreamFromUrl(Episode.Url, Episode.Signet);
      Episode.PlayState = EpisodeVM.PlayingState.Playing;
      OnPropertyChanged(() => ButtonPic);
    }

    private void UpdateProgress(TimeSpan signet)
    {
      CurrentProgress = signet;
    }

    private void MarkEpisodeComplete()
    {

    }

    private bool CanTogglePlay(object arg)
    {
       return ((Episode != null) && (StreamPlayer != null));
    }

    private void OnTogglePlay(object arg)
    {
      if (Episode.PlayState == EpisodeVM.PlayingState.Playing)
      {
        PauseCurrentEpisode();
      }
      else
      {
        PlayCurrentEpisode();
      }
    }

    private void RaiseCanExecuteChanged()
    {
      DelegateCommand<object> command = TogglePlayCommand as DelegateCommand<object>;
      command.RaiseCanExecuteChanged();
    }
    #endregion

  }
}
