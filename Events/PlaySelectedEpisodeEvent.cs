using Microsoft.Practices.Prism.PubSubEvents;
using PodCatchup.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PodCatchup.Events
{
  public class PlaySelectedEpisodeEvent : PubSubEvent<EpisodeVM>
  {
  }
}
