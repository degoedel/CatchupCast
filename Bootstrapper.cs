﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using CatchupCast.Modules;
using CatchupCast.ViewModel;
using Microsoft.Practices.Prism.Modularity;
using Microsoft.Practices.Prism.UnityExtensions;
using Microsoft.Practices.Unity;

namespace CatchupCast
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
      App.Current.MainWindow.Title = "Podcast Catchup Manager " + System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString(2);
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

  }
}