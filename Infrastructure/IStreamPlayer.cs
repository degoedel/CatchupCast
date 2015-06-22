using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PodCatchup.Infrastructure
{
  public interface IStreamPlayer
  {
    void StreamFromUrl(String surl, TimeSpan starttime);
    void PauseStream();
  }
}
