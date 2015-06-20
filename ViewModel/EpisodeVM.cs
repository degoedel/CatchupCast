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

namespace PodCatchup.ViewModel
{
  public class EpisodeVM
  {
    #region Members
    private Episode _episode;
    protected readonly IEventAggregator _eventAggregator;
    #endregion

    #region Constructors
    public EpisodeVM(IUnityContainer container)
    {
      Container = container;
      _eventAggregator = ApplicationService.Instance.EventAggregator;
      Episode = new Episode();
      PlayEpisodeCommand = new DelegateCommand<object>(this.OnPlayEpisode, this.CanPlayEpisode);
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
          if (_episode.Signet > 0)
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
          _episode.Signet = 0;
        }
        else if (value.CompareTo("Never") == 0)
        {
          LastPlayed = (new DateTime()).ToString();
          _episode.Signet = 0;
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
      _eventAggregator.GetEvent<PlaySelectedEpisodeEvent>().Publish(this);
    }
    #endregion

  }
}
