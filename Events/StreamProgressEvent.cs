using Microsoft.Practices.Prism.PubSubEvents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PodCatchup.Events
{
  public class StreamProgressEvent : PubSubEvent<TimeSpan>
  {
  }
}
