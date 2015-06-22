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
      PlayEpisodeCommand = new DelegateCommand<object>(this.OnPlayEpisode, this.CanPlayEpisode);
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
        return _episode.LastPlayed.ToString(); 
      }
      set { _episode.LastPlayed = DateTime.Parse(value); }
    }

    public String PlayStateAsStr
    {
      get
      {
        if (PlayState == PlayingState.Playing)
        {
          return ">";
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
        OnPropertyChanged(() => PlayStateAsStr);
      } 
    }

    public String State
    {
      get
      {
        if (LastPlayed.CompareTo("Never") == 0)
        {
          return "New";
        }
        else
        {
          if (_episode.Signet.TotalSeconds > 0)
          {
            return "In progress";
          }
          else
          {
            return "Done";
          }
        }

      }
      set
      {
        if (value.CompareTo("Done") == 0)
        {
          LastPlayed = DateTime.Today.ToString();
          _episode.Signet = TimeSpan.Parse("00:00:00");
        }
        else if (value.CompareTo("Never") == 0)
        {
          LastPlayed = (new DateTime()).ToString();
          _episode.Signet = TimeSpan.Parse("00:00:00");
        }
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
      get { return _episode.Signet; }
      set { _episode.Signet = value; } 
    }

    public String Url
    {
      get { return _episode.Url; }
      set { _episode.Url = value; }
    }

    #endregion

    #region CommandProperties
    public ICommand PlayEpisodeCommand { get; set; }
    #endregion

    #region Interactivity
    private bool CanPlayEpisode(object arg) 
    {
      return true;
    }

    private void OnPlayEpisode(object arg) 
    {
      if (PlayState == PlayingState.Stopped)
      {
        LastPlayed = DateTime.Today.ToString();
      }
      _eventAggregator.GetEvent<PlaySelectedEpisodeEvent>().Publish(this);
    }
    #endregion

  }
}
