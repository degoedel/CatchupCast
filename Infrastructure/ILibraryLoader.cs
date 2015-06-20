using PodCatchup.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PodCatchup.Infrastructure
{
  public interface ILibraryLoader
  {
    PodcastLibrary LoadLibrary();
  }
}
