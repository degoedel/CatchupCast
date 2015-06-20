using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Practices.Prism.Modularity;
using Microsoft.Practices.Unity;
using PodCatchup.Infrastructure;
using PodCatchup.Services;

namespace PodCatchup.Modules
{
  public class ExportModule : IModule
  {
    #region Constructors
    public ExportModule(IUnityContainer container)
    {
      Container = container;
    }
    #endregion

    #region IModule Members
    public void Initialize()
    {
      Container.RegisterType<ILibrarySaver, LiteDBLibrarySaver>();
    }
    #endregion

    #region Properties
    private IUnityContainer Container { get; set; }
    #endregion
  }
}
