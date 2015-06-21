using Microsoft.Practices.Prism.PubSubEvents;
using Microsoft.Practices.Unity;
using PodCatchup.Infrastructure;
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
    public PlayerVM(IUnityContainer container)
    {
      Container = container;
      _eventAggregator = ApplicationService.Instance.EventAggregator;
      this._eventAggregator.GetEvent<PlaySelectedEpisodeEvent>()
            .Subscribe((episode) => { this.PlayEpisode(episode); });
    }
    #endregion

    #region Properties
    private IUnityContainer Container { get; set; }
    public EpisodeVM Episode { get; set; }

    #endregion

    #region CommandProperties
    #endregion

    #region Interactivity
    private void PlayEpisode(EpisodeVM episode)
    {
      Episode = episode;
      IStreamPlayer player = Container.Resolve<IStreamPlayer>();
      player.StreamFromUrl(Episode.Url, (int)Episode.Signet);
    }
    #endregion

  }
}
