using Microsoft.Practices.Prism.PubSubEvents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PodCatchup.ViewModel
{
  public class PlayerVM
  {
    #region Members
    protected readonly IEventAggregator _eventAggregator;
    private EpisodeVM _episode;
    #endregion

    #region Constructors
    public PlayerVM()
    {
      _eventAggregator = ApplicationService.Instance.EventAggregator;
      this._eventAggregator.GetEvent<PlaySelectedEpisodeEvent>()
            .Subscribe((episode) => { this.Episode = episode; });
    }
    #endregion

    #region Properties
    public EpisodeVM Episode { get; set; }
    #endregion

    #region CommandProperties
    #endregion

    #region Interactivity
    #endregion

  }
}
