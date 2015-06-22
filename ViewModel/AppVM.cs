using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Prism.Mvvm;
using PodCatchup.Infrastructure;
using PodCatchup.Model;
using PodCatchup.Events;

namespace PodCatchup.ViewModel
{
  public class AppVM : BindableBase
  {
    #region Members
    PodcastLibraryVM _library;
    PlayerVM _player;
    #endregion

    #region Constructors
    public AppVM(IUnityContainer container)
    {
      Container = container;
      _library = new PodcastLibraryVM(container);
      _player = new PlayerVM(container);
      AppDomain.CurrentDomain.ProcessExit += new EventHandler(OnProcessExit);
    }
    #endregion

    #region Properties
    private IUnityContainer Container { get; set; }
    public PodcastLibraryVM Library
    {
      get { return _library; }
      set { SetProperty(ref this._library, value); }
    }

    public PlayerVM Player
    {
      get { return _player; }
      set { SetProperty(ref this._player, value); }
    }
    #endregion

    #region CommandProperties
    #endregion

    #region Interactivity
    public void LoadData()
    {
      ILibraryLoader loader = Container.Resolve<ILibraryLoader>();
      PodcastLibrary lib = loader.LoadLibrary();
      Library.Library = lib;
    }

    private void OnProcessExit(object sender, EventArgs e)
    {
      Player.PauseCurrentEpisode();
      ILibrarySaver saver = Container.Resolve<ILibrarySaver>();
      PodcastLibrary lib = _library.Library;
      saver.SaveLibrary(ref lib);
    }
    #endregion
  }
}
