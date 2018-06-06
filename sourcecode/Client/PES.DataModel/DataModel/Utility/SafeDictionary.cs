using System;
using System.Collections.Generic;
using System.Linq;

namespace PES.DataModel
{
    #region SafeDictionary

    internal class SafeDictionary<TKey, TValue> : IDisposable
    {
        private readonly Dictionary<TKey, TValue> dictionary = new Dictionary<TKey, TValue>();
        private readonly object o = new object();

        public IEnumerable<TKey> Keys
        {
            get
            {
                return dictionary.Keys;
            }
        }

        public TValue this[TKey key]
        {
            set
            {
                lock (o)
                {
                    TValue current;
                    if (dictionary.TryGetValue(key, out current))
                    {
                        var disposable = current as IDisposable;

                        if (disposable != null)
                            disposable.Dispose();
                    }

                    dictionary[key] = value;
                }
            }
        }

        public void Clear()
        {
            lock (o)
            {
                dictionary.Clear();
            }
        }

        public bool Remove(TKey key)
        {
            lock (o)
            {
                return dictionary.Remove(key);
            }
        }

        public bool TryGetValue(TKey key, out TValue value)
        {
            lock (o)
            {
                return dictionary.TryGetValue(key, out value);
            }
        }

        #region IDisposable Members

        public void Dispose()
        {
            lock (o)
            {
                var disposableItems = from item in dictionary.Values
                                      where item is IDisposable
                                      select item as IDisposable;

                foreach (var item in disposableItems)
                {
                    item.Dispose();
                }
            }

            GC.SuppressFinalize(this);
        }

        #endregion IDisposable Members
    }

    #endregion SafeDictionary
}