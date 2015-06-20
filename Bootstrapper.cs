using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using PodCatchup.Modules;
using PodCatchup.ViewModel;
using Microsoft.Practices.Prism.Modularity;
using Microsoft.Practices.Prism.UnityExtensions;
using Microsoft.Practices.Unity;

namespace PodCatchup
{
  public class Bootstrapper : UnityBootstrapper
  {
    protected override System.Windows.DependencyObject CreateShell()
    {
      return new MainWindow();
    }

    protected override void InitializeShell()
    {
      base.InitializeShell();
      App.Current.MainWindow = (Window)this.Shell;
      App.Current.MainWindow.DataContext = ((IUnityContainer)Container).Resolve<AppVM>();
      App.Current.MainWindow.Title = "PodCatchup : Podcast Catchup Manager " + System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString(2);
      App.Current.MainWindow.Show();
    }

    protected override IModuleCatalog CreateModuleCatalog()
    {
      ModuleCatalog catalog = new ModuleCatalog();

      catalog.AddModule(typeof(ExportModule));
      catalog.AddModule(typeof(ImportModule));
      catalog.AddModule(typeof(InteractivityModule));
      return catalog;
    }

    public override void Run(bool runWithDefaultConfiguration)
    {
      base.Run(runWithDefaultConfiguration);
      AppVM app = (AppVM)App.Current.MainWindow.DataContext;
      app.LoadData();
      // modules (and everything else) have been initialized when you get here
    }

  }
}
