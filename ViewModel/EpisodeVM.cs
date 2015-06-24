using PodCatchup.Model;
using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Practices.Prism.PubSubEvents;
using System.Windows.Input;
using Microsoft.Practices.Prism.Commands;
using PodCatchup.Events;
using Microsoft.Practices.Prism.Mvvm;

namespace PodCatchup.ViewModel
{
  public class EpisodeVM : BindableBase
  {
    public enum PlayingState { Stopped, Playing };
    public enum EpisodeState { New, InProgress, Done };

    #region Members
    private Episode _episode;
    protected readonly IEventAggregator _eventAggregator;
    private PlayingState _playState;
    #endregion

    #region Constructors
    public EpisodeVM(IUnityContainer container)
    {
      Container = container;
      _eventAggregator = ApplicationService.Instance.EventAggregator;
      Episode = new Episode();
      PlayPauseEpisodeCommand = new DelegateCommand<object>(this.OnPlayPauseEpisode, this.CanPlayPauseEpisode);
      _playState = PlayingState.Stopped;
    }
    #endregion

    #region Properties
    private IUnityContainer Container { get; set; }
 
    public Episode Episode 
    { 
      get { return _episode; }
      set { _episode = value; }
    }

    public String Title
    {
      get { return _episode.Title; }
      set { _episode.Title = value; }
    }

    public String Published
    {
      get { return _episode.Published.ToString(); }
      set { _episode.Published = DateTime.Parse(value); }
    }

    public String LastPlayed
    {
      get 
      {
        DateTime origin = new DateTime();
        if (_episode.LastPlayed.CompareTo(origin) == 0)
        {
          return "Never";
        }
        return _episode.LastPlayed.ToString("yyyy/MM/dd"); 
      }
      set 
      { 
        _episode.LastPlayed = DateTime.Parse(value);
        OnPropertyChanged(() => LastPlayed);
      }
    }

    public String PlayStateAsPic
    {
      get
      {
        if (PlayState == PlayingState.Playing)
        {
          return "/PodCatchup;component/icons/Play.png";
        }
        else
        {
          return "";
        }
      }
      set { }
    }

    public PlayingState PlayState 
    {
      get { return _playState; }
      set 
      { 
        SetProperty(ref this._playState, value);
        OnPropertyChanged(() => ButtonPic);
        OnPropertyChanged(() => PlayStateAsPic);
        OnPropertyChanged(() => StateAsStr);
        OnPropertyChanged(() => LastPlayed);
      } 
    }

    public EpisodeState State
    {
      get
      {
        if (LastPlayed.CompareTo("Never") == 0)
        {
          return EpisodeState.New;
        }
        else
        {
          if (_episode.Signet > 0)
          {
            return EpisodeState.InProgress;
          }
          else
          {
            return EpisodeState.Done;
          }
        }

      }
      set
      {
        if (PlayState == PlayingState.Playing)
        {
          OnPlayPauseEpisode(null);
        }
        if (value == EpisodeState.Done)
        {
          LastPlayed = DateTime.Today.ToString();
          _episode.Signet = 0;
        }
        else if (value == EpisodeState.New)
        {
          LastPlayed = (new DateTime()).ToString();
          _episode.Signet = 0;
        }
        OnPropertyChanged(() => StateAsStr);
        OnPropertyChanged(() => ButtonPic);
      }
    }

    public String StateAsStr
    {
      get
      {
        switch (State)
        {
          case EpisodeState.Done:
            return "Done";
          case EpisodeState.InProgress:
            return "In progress";
          case EpisodeState.New:
            return "New";
          default:
            return "";
        }
      }
      set
      {
      }
    }

    public String Cover
    {
      get { return _episode.Cover; }
      set { _episode.Cover = value; }
    }

    public String Duration
    {
      get { return _episode.Duration; }
      set { _episode.Duration = value; }
    }

    public String SubTitle
    {
      get { return _episode.Subtitle; }
      set { _episode.Subtitle = value; }
    }

    public String Summary
    {
      get { return _episode.Summary; }
      set { _episode.Summary = value; }
    }

    public TimeSpan Signet 
    {
      get { return TimeSpan.FromSeconds(_episode.Signet); }
      set 
      { 
        _episode.Signet = value.TotalSeconds;
        OnPropertyChanged(() => State);
      } 
    }

    public String Url
    {
      get { return _episode.Url; }
      set { _episode.Url = value; }
    }

    public String ButtonPic
    {
      get
      {
        if (_playState == PlayingState.Playing)
          {
            return "/PodCatchup;component/icons/Pause2.png";
          }
          else
          {
            return "/PodCatchup;component/icons/Play2.png";
          }
      }
      set
      {

      }
    }
    #endregion

    #region CommandProperties
    public ICommand PlayPauseEpisodeCommand { get; set; }
    #endregion

    #region Interactivity
    private bool CanPlayPauseEpisode(object arg) 
    {
      return true;
    }

    private void OnPlayPauseEpisode(object arg) 
    {
      if (PlayState == PlayingState.Stopped)
      {
        LastPlayed = DateTime.Today.ToString();
      }
      _eventAggregator.GetEvent<PlayPauseSelectedEpisodeEvent>().Publish(this);
    }
    #endregion

  }
}
