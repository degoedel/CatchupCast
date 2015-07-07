using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace PodCatchup.ViewModel
{
  public class ObservableEpisodesCollection : ObservableCollection<EpisodeVM>
  {
    ICollectionView _view;

    public ObservableEpisodesCollection()
    {
        _view = CollectionViewSource.GetDefaultView(this);                        
    }

    public void Sort(string propertyName, ListSortDirection sortDirection)
    {
        _view.SortDescriptions.Clear();
        _view.SortDescriptions.Add(new SortDescription(propertyName, sortDirection));
    }

    protected override void OnCollectionChanged(NotifyCollectionChangedEventArgs e)
    {            
        switch (e.Action)
        {
            case NotifyCollectionChangedAction.Add:
                this.AddPropertyChanged(e.NewItems);
                break;

            case NotifyCollectionChangedAction.Remove:
                this.RemovePropertyChanged(e.OldItems);
                break;

            case NotifyCollectionChangedAction.Replace:
            case NotifyCollectionChangedAction.Reset:
                this.RemovePropertyChanged(e.OldItems);
                this.AddPropertyChanged(e.NewItems);
                break;
        }

        base.OnCollectionChanged(e);
    }

    private void AddPropertyChanged(IEnumerable items)
    {
        if (items != null)
        {
            foreach (var obj in items.OfType<INotifyPropertyChanged>())
            {
                obj.PropertyChanged += OnItemPropertyChanged;
            }
        }
    }

    private void RemovePropertyChanged(IEnumerable items)
    {
        if (items != null)
        {
            foreach (var obj in items.OfType<INotifyPropertyChanged>())
            {
                obj.PropertyChanged -= OnItemPropertyChanged;
            }
        }
    }

    private void OnItemPropertyChanged(object sender, PropertyChangedEventArgs e)
    {
        bool sortedPropertyChanged = false;
        foreach (SortDescription sortDescription in _view.SortDescriptions)
        {
            if (sortDescription.PropertyName == e.PropertyName)
                sortedPropertyChanged = true;
        }

        if (sortedPropertyChanged)
        {                
            NotifyCollectionChangedEventArgs arg = new NotifyCollectionChangedEventArgs(
                NotifyCollectionChangedAction.Replace, sender, sender, this.Items.IndexOf((EpisodeVM)sender));

            OnCollectionChanged(arg);          
        }
    }
  }
}
