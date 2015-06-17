using CatchupCast.Model;
using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatchupCast.ViewModel
{
  public class EpisodeVM
  {
    #region Members
    private Episode _episode;
    #endregion

    #region Constructors
    public EpisodeVM(IUnityContainer container)
    {
      Container = container;
      Episode = new Episode();
    }
    #endregion

    #region Properties
    private IUnityContainer Container { get; set; }

    public Episode Episode 
    { 
      get { return _episode; }
      set { _episode = value; }
    }
    #endregion

    #region CommandProperties
    #endregion

    #region Interactivity
    #endregion

  }
}
