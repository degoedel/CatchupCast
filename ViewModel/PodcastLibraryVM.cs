using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PodCatchup.Model;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.ViewModel;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Prism.Mvvm;
using System.ServiceModel.Syndication;
using System.Windows.Input;
using System.Xml;
using System.Windows;

namespace PodCatchup.ViewModel
{
  public class PodcastLibraryVM : BindableBase
  {
    #region Members
    private PodcastLibrary _library;
    private String _newurl;
    ObservableCollection<PodcastVM> _podcasts;
    private PodcastVM _selecteditem;
    #endregion

    #region Constructors
    public PodcastLibraryVM(IUnityContainer container)
    {
      Container = container;
      _library = new PodcastLibrary();
      _newurl = "";
      _podcasts = new ObservableCollection<PodcastVM>();
      _selecteditem = null;

      RefreshLibrary();
      AddPodcastCommand = new DelegateCommand<object>(this.OnAddPodcast, this.CanAddPodcast);
      RemoveItemCommand = new DelegateCommand<object>(this.OnRemoveItem, this.CanRemoveItem);
    }
    #endregion

    #region Properties
    private IUnityContainer Container { get; set; }

    public PodcastLibrary Library
    {
      get { return _library; }
      set 
      { 
        SetProperty(ref this._library, value);
        RefreshLibrary();
      }
    }

    public String NewUrl
    {
      get { return _newurl; }
      set 
      { 
        SetProperty(ref this._newurl, value);
        RaiseCanExecuteChanged();
      }
    }

    public ObservableCollection<PodcastVM> Podcasts
    {
      get { return _podcasts; }
      set { SetProperty(ref this._podcasts, value); }
    }

    public PodcastVM SelectedItem
    {
      get { return _selecteditem; }
      set 
      { 
        SetProperty(ref this._selecteditem, value);
        _selecteditem.RefreshEpisodes();
      }
    }
    #endregion

    #region CommandProperties
    public ICommand AddPodcastCommand { get; set; }
    public ICommand RemoveItemCommand { get; set; }
    #endregion

    #region Interactivity
    private void OnAddPodcast(object arg)   
    {
      if (_library.Library.ContainsKey(NewUrl))
      {
        MessageBox.Show("This Podcast is already in the library.");
        NewUrl = "";
        return;
      }
      PodcastVM npvm = Container.Resolve<PodcastVM>();
      npvm.Syndication = _newurl;
      _library.Library.Add(npvm.Syndication, npvm.Podcast);
      Podcasts.Add(npvm);
      NewUrl = "";
      SelectedItem = npvm;

    }

    private bool CanAddPodcast(object arg) 
    {
      try
      {
        SyndicationFeed feed = SyndicationFeed.Load(XmlReader.Create(NewUrl));
        return true;
      }
      catch (Exception)
      {
        return false;
      }
    }

    private void OnRemoveItem(object arg)
    {
    }

    private bool CanRemoveItem(object arg)
    {
      return true;
    }

    private void RaiseCanExecuteChanged()
    {
        DelegateCommand<object> command = AddPodcastCommand as DelegateCommand<object>;
        command.RaiseCanExecuteChanged();
    }

    private void RefreshLibrary()
    {
      foreach (Podcast p in _library.Library.Values)
      {
        PodcastVM pvm = Container.Resolve<PodcastVM>();
        pvm.Podcast = p;
        Podcasts.Add(pvm);
      }
    }

    #endregion

  }
}
