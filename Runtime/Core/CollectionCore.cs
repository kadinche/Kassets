using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Kadinche.Kassets.Variable;

namespace Kadinche.Kassets.Collection
{
    public abstract partial class Collection<T> : VariableCore<List<T>>, IList<T>
    {
        #region Property

        public override List<T> Value
        {
            get => _value;
            set => Copy(value);
        }

        #endregion

        private T _lastRemoved;

        #region Interface Implementation

        public IEnumerator<T> GetEnumerator() => _value.GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => _value.GetEnumerator();

        public virtual void Add(T item)
        {
            _value.Add(item);
            RaiseOnAdd(item);
            var index = _value.Count - 1;
            RaiseValueAt(index, item);
        }

        public virtual void Copy(IEnumerable<T> others)
        {
            _value.Clear();
            _value.AddRange(others);
        }

        public virtual void Clear()
        {
            ClearValueSubscriptions();
            _value.Clear();
            RaiseOnClear();
        }

        public virtual bool Remove(T item)
        {
            var idx = _value.IndexOf(item);
            var removed = _value.Remove(item);
            if (removed)
            {
                RemoveValueSubscription(idx);
                RaiseOnRemove(item);
                _lastRemoved = item;
            }

            return removed;
        }

        public virtual void Insert(int index, T item)
        {
            _value.Insert(index, item);
            RaiseOnAdd(item);
        }

        public virtual void RemoveAt(int index)
        {
            _lastRemoved = _value[index];
            _value.RemoveAt(index);
            RaiseOnRemove(_lastRemoved);
            RemoveValueSubscription(index);
        }

        public T this[int index]
        {
            get => _value[index];
            set
            {
                _value[index] = value;
                RaiseValueAt(index, value);
            }
        }

        public bool Contains(T item) => _value.Contains(item);
        public void CopyTo(T[] array, int arrayIndex) => _value.CopyTo(array, arrayIndex);
        public int Count => _value.Count;
        bool ICollection<T>.IsReadOnly => _value.ToArray().IsReadOnly;
        public int IndexOf(T item) => _value.IndexOf(item);

        #endregion

        #region Method Overload

        private readonly List<T> _initialValue = new List<T>();
        public override List<T> InitialValue
        {
            get => _initialValue;
            protected set
            {
                _initialValue.Clear();
                _initialValue.AddRange(value);
            }
        }

        public override void Dispose()
        {
            base.Dispose();
            DisposeSubscriptions();
        }

        #endregion
    }

    public abstract partial class Collection<TKey, TValue> : Collection<SerializedKeyValuePair<TKey, TValue>>,
        IDictionary<TKey, TValue>
    {
        #region Field and Property

        private readonly Dictionary<TKey, TValue> _activeDictionary = new Dictionary<TKey, TValue>();

        public override List<SerializedKeyValuePair<TKey, TValue>> Value
        {
            get => _value;
            set
            {
                _value.Clear();
                _value.AddRange(value);

                _activeDictionary.Clear();
                foreach (var pair in value)
                {
                    _activeDictionary.Add(pair.key, pair.value);
                }
            }
        }

        #endregion

        #region Interface Implementation

        public override void Add(SerializedKeyValuePair<TKey, TValue> item)
        {
            _activeDictionary.Add(item.key, item.value);
            base.Add(item);
            RaiseValue(item.key, item.value);
        }

        public void Add(TKey key, TValue value) => Add(new SerializedKeyValuePair<TKey, TValue>(key, value));

        public override void Clear()
        {
            ClearValueSubscriptions();
            Value.Clear();
            base.Clear();
        }

        public override bool Remove(SerializedKeyValuePair<TKey, TValue> item)
        {
            var removed = _activeDictionary.Remove(item.key);
            RemoveValueSubscription(item.key);
            return removed && base.Remove(item);
        }

        public bool Remove(TKey key)
        {
            var toRemove = _value.First(pair => pair.key.Equals(key));
            RemoveValueSubscription(key);
            return Remove(toRemove);
        }

        public bool TryGetValue(TKey key, out TValue value) => _activeDictionary.TryGetValue(key, out value);

        public TValue this[TKey key]
        {
            get => _activeDictionary[key];
            set
            {
                _activeDictionary[key] = value;
                var _ = _value.First(p => p.key.Equals(key));
                _.value = value;
                RaiseValue(key, value);
            }
        }

        public new int Count => Value.Count;
        public bool ContainsKey(TKey key) => _activeDictionary.ContainsKey(key);
        public bool ContainsValue(TValue value) => _activeDictionary.ContainsValue(value);

        public new IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator() => _activeDictionary.GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => Value.GetEnumerator();

        void ICollection<KeyValuePair<TKey, TValue>>.Add(KeyValuePair<TKey, TValue> item) => Add(item.Key, item.Value);

        bool ICollection<KeyValuePair<TKey, TValue>>.Contains(KeyValuePair<TKey, TValue> item) =>
            _activeDictionary.ContainsKey(item.Key) && _activeDictionary.ContainsValue(item.Value);

        void ICollection<KeyValuePair<TKey, TValue>>.CopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex)
        {
            _activeDictionary.ToList().CopyTo(array, arrayIndex);
        }

        bool ICollection<KeyValuePair<TKey, TValue>>.Remove(KeyValuePair<TKey, TValue> item) => Remove(item.Key);
        bool ICollection<KeyValuePair<TKey, TValue>>.IsReadOnly => _activeDictionary.ToArray().IsReadOnly;

        public ICollection<TKey> Keys => _activeDictionary.Keys;
        public ICollection<TValue> Values => _activeDictionary.Values;

        #endregion

        public override void Dispose()
        {
            base.Dispose();
            ClearValueSubscriptions();
        }
    }

    [Serializable]
    public struct SerializedKeyValuePair<TKey, TValue>
    {
        public TKey key;
        public TValue value;
    
        public SerializedKeyValuePair(TKey key, TValue value)
        {
            this.key = key;
            this.value = value;
        }
        
        public static implicit operator KeyValuePair<TKey, TValue>(SerializedKeyValuePair<TKey, TValue> serializedKeyValuePair) => new KeyValuePair<TKey, TValue>(serializedKeyValuePair.key, serializedKeyValuePair.value);
    }

#if !KASSETS_UNIRX && !KASSETS_UNITASK
    public abstract partial class Collection<T>
    {
        private readonly IList<IDisposable> _onAddSubscriptions = new List<IDisposable>();
        private readonly IList<IDisposable> _onRemoveSubscriptions = new List<IDisposable>();
        private readonly IList<IDisposable> _onClearSubscriptions = new List<IDisposable>();
        private readonly IDictionary<int, IList<IDisposable>> _valueSubscriptions = new Dictionary<int, IList<IDisposable>>();
        
        public IDisposable SubscribeOnAdd(Action<T> action) => SubscribeOnAdd(action, buffered);
        public IDisposable SubscribeOnRemove(Action<T> action) => SubscribeOnRemove(action, buffered);
        public IDisposable SubscribeOnClear(Action action) => SubscribeOnClear(action, buffered);
        public IDisposable SubscribeToValueAt(int index, Action<T> action) => SubscribeToValueAt(index, action, buffered);

        public IDisposable SubscribeOnAdd(Action<T> action, bool withBuffer)
        {
            var subscription = new Subscription<T>(action, _onAddSubscriptions);
            if (!_onAddSubscriptions.Contains(subscription))
            {
                _onAddSubscriptions.Add(subscription);
                if (withBuffer && _value.Count > 0)
                {
                    subscription.Invoke(_value.LastOrDefault());
                }
            }

            return subscription;
        }

        public IDisposable SubscribeOnRemove(Action<T> action, bool withBuffer)
        {
            var subscription = new Subscription<T>(action, _onRemoveSubscriptions);
            if (!_onRemoveSubscriptions.Contains(subscription))
            {
                _onRemoveSubscriptions.Add(subscription);
                if (withBuffer)
                {
                    subscription.Invoke(_lastRemoved);
                }
            }

            return subscription;
        }

        public IDisposable SubscribeOnClear(Action action, bool withBuffer)
        {
            var subscription = new Subscription(action, _onClearSubscriptions);
            if (!_onClearSubscriptions.Contains(subscription))
            {
                _onClearSubscriptions.Add(subscription);
                if (withBuffer)
                {
                    subscription.Invoke();
                }
            }

            return subscription;
        }

        public IDisposable SubscribeToValueAt(int index, Action<T> action, bool withBuffer)
        {
            if (!_valueSubscriptions.TryGetValue(index, out var subscriptions))
            {
                subscriptions = new List<IDisposable>();
                _valueSubscriptions.Add(index, subscriptions);
            }
            
            var subscription = new Subscription<T>(action, subscriptions);
            if (!subscriptions.Contains(subscription))
            {
                subscriptions.Add(subscription);
                if (_value[index] != null && withBuffer)
                {
                    subscription.Invoke(_value[index]);
                }
            }

            return subscription;
        }
        
        private void RaiseOnAdd(T addedValue)
        {
            foreach (var disposable in _onAddSubscriptions)
            {
                if (disposable is Subscription<T> valueSubscription)
                    valueSubscription.Invoke(addedValue);
            }
        }
        
        private void RaiseOnRemove(T removedValue)
        {
            foreach (var disposable in _onRemoveSubscriptions)
            {
                if (disposable is Subscription<T> valueSubscription)
                    valueSubscription.Invoke(removedValue);
            }
        }
        
        private void RaiseOnClear()
        {
            foreach (var disposable in _onClearSubscriptions)
            {
                if (disposable is Subscription subscription)
                    subscription.Invoke();
            }
        }

        private void RaiseValueAt(int index, T value)
        {
            if (_variableEventType == VariableEventType.ValueChange && _value[index].Equals(value))
                return;

            if (_valueSubscriptions.TryGetValue(index, out var subscriptions))
            {
                foreach (var disposable in subscriptions)
                {
                    if (disposable is Subscription<T> valueSubscription)
                        valueSubscription.Invoke(value);
                }
            }
        }
        
        private void ClearValueSubscriptions()
        {
            foreach (var subscriptions in _valueSubscriptions.Values)
            {
                foreach (var disposable in subscriptions)
                {
                    disposable.Dispose();
                }
                subscriptions.Clear();
            }
            _valueSubscriptions.Clear();
        }
        
        private void RemoveValueSubscription(int index)
        {
            if (_valueSubscriptions.TryGetValue(index, out var subscriptions))
            {
                subscriptions.Dispose();
                _valueSubscriptions.Remove(index);
            }
        }

        private void DisposeSubscriptions()
        {
            _onAddSubscriptions.Dispose();
            _onRemoveSubscriptions.Dispose();
            _onClearSubscriptions.Dispose();
            ClearValueSubscriptions();
        }
    }
    
    public abstract partial class Collection<TKey, TValue>
    {
        private readonly IDictionary<TKey, IList<IDisposable>> _valueSubscriptions = new Dictionary<TKey, IList<IDisposable>>();

        public IDisposable SubscribeOnAdd(Action<TKey, TValue> action, bool withBuffer) => SubscribeOnAdd(pair => action.Invoke(pair.key, pair.value), withBuffer);
        public IDisposable SubscribeOnAdd(Action<TKey, TValue> action) => SubscribeOnAdd(action, buffered);
        
        public IDisposable SubscribeOnRemove(Action<TKey, TValue> action, bool withBuffer) => SubscribeOnRemove(pair => action.Invoke(pair.key, pair.value), withBuffer);
        public IDisposable SubscribeOnRemove(Action<TKey, TValue> action) => SubscribeOnRemove(action, buffered);
        
        public IDisposable SubscribeToValue(TKey key, Action<TValue> action) => SubscribeToValue(key, action, buffered);

        public IDisposable SubscribeToValue(TKey key, Action<TValue> action, bool withBuffer)
        {
            if (!_valueSubscriptions.TryGetValue(key, out var subscriptions))
            {
                subscriptions = new List<IDisposable>();
                _valueSubscriptions.Add(key, subscriptions);
            }
            
            var subscription = new Subscription<TValue>(action, subscriptions);
            if (!subscriptions.Contains(subscription))
            {
                subscriptions.Add(subscription);
                if (_activeDictionary[key] != null && withBuffer)
                {
                    subscription.Invoke(_activeDictionary[key]);
                }
            }

            return subscription;
        }
        
        private void RaiseValue(TKey key, TValue value)
        {
            if (_variableEventType == VariableEventType.ValueChange && _activeDictionary[key].Equals(value))
                return;

            if (_valueSubscriptions.TryGetValue(key, out var subscriptions))
            {
                foreach (var disposable in subscriptions)
                {
                    if (disposable is Subscription<TValue> valueSubscription)
                        valueSubscription.Invoke(value);
                }
            }
        }

        private void ClearValueSubscriptions()
        {
            foreach (var subscriptions in _valueSubscriptions.Values)
            {
                foreach (var disposable in subscriptions)
                {
                    disposable.Dispose();
                }
                subscriptions.Clear();
            }
            _valueSubscriptions.Clear();
        }

        private void RemoveValueSubscription(TKey key)
        {
            if (_valueSubscriptions.TryGetValue(key, out var subscriptions))
            {
                subscriptions.Dispose();
                _valueSubscriptions.Remove(key);
            }
        }
    }
#endif
}