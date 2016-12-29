using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using Core.Common.Core;
using Core.Common.Utils;

namespace Core.Common.Extensions
{
    public static class CoreExtensionsOld
    {
        public static void Merge<T>(this ObservableCollection<T> source, IEnumerable<T> collection)
        {
            Merge<T>(source, collection, false);
        }

        public static void Merge<T>(this ObservableCollection<T> source, IEnumerable<T> collection, bool ignoreDuplicates)
        {
            if (collection != null)
            {
                foreach (T item in collection)
                {
                    bool addItem = true;

                    if (ignoreDuplicates)
                        addItem = !source.Contains(item);

                    if (addItem)
                        source.Add(item);
                }
            }
        }
    }
}
