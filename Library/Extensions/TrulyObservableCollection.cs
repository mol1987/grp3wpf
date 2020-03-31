using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Runtime.CompilerServices;

/// <summary>
/// Extension methods/classes
/// </summary>
namespace Library.Extensions
{
    public class ObservableObject : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propName = null)
        {
            var handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propName));
            }
        }

        protected bool SetProperty<T>(ref T storage, T value, [CallerMemberName] string propertyName = null)
        {
            if (Equals(storage, value)) return false;
            storage = value;
            OnPropertyChanged(propertyName);
            return true;
        }
    }
    //public static class ObservableCollectionExtension
    //{
    //    public static void NotifyPropertyChanged<T>(this ObservableCollection<T> observableCollection, Action<T, PropertyChangedEventArgs> callBackAction)
    //        where T : INotifyPropertyChanged
    //    {
    //        observableCollection.CollectionChanged += (sender, args) =>
    //        {
    //            //Does not prevent garbage collection says: http://stackoverflow.com/questions/298261/do-event-handlers-stop-garbage-collection-from-occuring
    //            //publisher.SomeEvent += target.SomeHandler;
    //            //then "publisher" will keep "target" alive, but "target" will not keep "publisher" alive.
    //            if (args.NewItems == null)
    //                return;

    //            foreach (T item in args.NewItems)
    //            {
    //                item.PropertyChanged += (obj, eventArgs) =>
    //                {
    //                    callBackAction((T)obj, eventArgs);
    //                };
    //            }
    //        };
    //    }
    //}
}
