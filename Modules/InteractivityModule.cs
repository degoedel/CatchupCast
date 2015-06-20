using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PodCatchup.Infrastructure;
using PodCatchup.Services;
using Microsoft.Practices.Prism.Modularity;
using Microsoft.Practices.Unity;

namespace PodCatchup.Modules
{
  public class InteractivityModule : IModule
  {
    #region Constructors
    public InteractivityModule(IUnityContainer container)
    {
      Container = container;
    }
    #endregion

    #region IModule Members
    public void Initialize()
    {
      Container.RegisterType<ISyndicationAnalyzer, SyndicationAnalyzer>();
    }
    #endregion

    #region Properties
    private IUnityContainer Container { get; set; }
    #endregion
  }
}
