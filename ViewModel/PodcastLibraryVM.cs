using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CatchupCast.Model;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.ViewModel;
using Microsoft.Practices.Unity;

namespace CatchupCast.ViewModel
{
  public class PodcastLibraryVM : NotificationObject
  {
    #region Members
    private PodcastLibrary _library;
    private String _newurl;
    ObservableCollection<PodcastVM> _podcasts;
    #endregion

    #region Constructors
    public PodcastLibraryVM(IUnityContainer container)
    {
      Container = container;
      _library = new PodcastLibrary();
      _newurl = "";
      _podcasts = new ObservableCollection<PodcastVM>();

      foreach (Podcast p in _library.Library.Values)
      {
        PodcastVM pvm = Container.Resolve<PodcastVM>();
        pvm.Podcast = p;
        _podcasts.Add(pvm);
      }

      AddPodcastCommand = new DelegateCommand(() =>
      {
        PodcastVM npvm = Container.Resolve<PodcastVM>();
        npvm.Syndication = _newurl;
        _library.Library.Add(npvm.Title, npvm.Podcast);
      }
      );
    }
    #endregion

    #region Properties
    private IUnityContainer Container { get; set; }

    public PodcastLibrary Library
    {
      get { return _library; }
      set { _library = value; }
    }

    public String NewUrl
    {
      get { return _newurl; }
      set 
      { 
        _newurl = value;
        RaisePropertyChanged("NewUrl");
      }
    }

    public ObservableCollection<PodcastVM> Podcasts
    {
      get { return _podcasts; }
      set { _podcasts = value; }
    }
    #endregion

    #region CommandProperties
    public DelegateCommand AddPodcastCommand { get; set; }
    #endregion

    #region Interactivity
    #endregion

  }
}
