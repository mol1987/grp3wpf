using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;

/// <summary>
/// Extension methods/classes
/// </summary>
namespace Library.Extensions
{
    public static class ObservableCollectionExtension
    {
        public static void NotifyPropertyChanged<T>(this ObservableCollection<T> observableCollection, Action<T, PropertyChangedEventArgs> callBackAction)
            where T : INotifyPropertyChanged
        {
            observableCollection.CollectionChanged += (sender, args) =>
            {
                //Does not prevent garbage collection says: http://stackoverflow.com/questions/298261/do-event-handlers-stop-garbage-collection-from-occuring
                //publisher.SomeEvent += target.SomeHandler;
                //then "publisher" will keep "target" alive, but "target" will not keep "publisher" alive.
                if (args.NewItems == null)
                    return;

                foreach (T item in args.NewItems)
                {
                    item.PropertyChanged += (obj, eventArgs) =>
                    {
                        callBackAction((T)obj, eventArgs);
                    };
                }
            };
        }
    }
}
