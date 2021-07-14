using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Kadinche.Kassets.EventSystem;
using UnityEngine;

namespace Kadinche.Kassets.Collection
{
    public abstract class Collection<T> : GameEventBase<T>, IList<T>
    {
        [SerializeField] private List<T> _values = new List<T>();
        private readonly List<T> _nonSerializedValues = new List<T>();

        [Tooltip("If true will reset collection contents when play mode end.")]
        [SerializeField] private bool _autoResetValues;
        
        private IList<T> ActiveList => _autoResetValues ? _nonSerializedValues : _values;

        private readonly IList<Subscription<T>> _onAddSubscribers = new List<Subscription<T>>();
        private readonly IList<Subscription<T>> _onRemoveSubscribers = new List<Subscription<T>>();
        private readonly IList<Subscription> _onClearSubscribers = new List<Subscription>();
        
        public IDisposable SubscribeOnAdd(Action<T> action)
        {
            var subscriber = new Subscription<T>(action, disposables);
            if (!_onAddSubscribers.Contains(subscriber) && !disposables.Contains(subscriber))
            {
                _onAddSubscribers.Add(subscriber);
                disposables.Add(subscriber);
            }

            return subscriber;
        }

        public IDisposable SubscribeOnRemove(Action<T> action)
        {
            var subscriber = new Subscription<T>(action, disposables);
            if (!_onRemoveSubscribers.Contains(subscriber) && !disposables.Contains(subscriber))
            {
                _onRemoveSubscribers.Add(subscriber);
                disposables.Add(subscriber);
            }

            return subscriber;
        }
        
        public IDisposable SubscribeOnClear(Action action)
        {
            var subscriber = new Subscription(action, disposables);
            if (!_onClearSubscribers.Contains(subscriber) && !disposables.Contains(subscriber))
            {
                _onClearSubscribers.Add(subscriber);
                disposables.Add(subscriber);
            }

            return subscriber;
        }

        public override IDisposable Subscribe(Action<T> action)
        {
            var compositeDisposable = new CompositeDisposable();
                compositeDisposable.Add(SubscribeOnAdd(action));
                compositeDisposable.Add(SubscribeOnRemove(action));
                compositeDisposable.Add(SubscribeOnClear(() => action.Invoke(bufferedValue)));
                
            return compositeDisposable;
        }

        private void RaiseOnAdd(T addedValue)
        {
            foreach (var subscriber in _onAddSubscribers)
            {
                subscriber.Invoke(addedValue);
            }
        }
        
        private void RaiseOnRemove(T removedValue)
        {
            foreach (var subscriber in _onRemoveSubscribers)
            {
                subscriber.Invoke(removedValue);
            }
        }
        
        private void RaiseOnClear()
        {
            foreach (var subscriber in _onClearSubscribers)
            {
                subscriber.Invoke();
            }
        }
        
        public IEnumerator<T> GetEnumerator() => ActiveList.GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => ActiveList.GetEnumerator();

        public void Add(T item)
        {
            bufferedValue = item;
            ActiveList.Add(item);
            RaiseOnAdd(item);
        }

        public void Copy(IEnumerable<T> others)
        {
            ActiveList.Clear();
            foreach (var other in others)
            {
                ActiveList.Add(other);
            }
        }
        
        public void Clear()
        {
            // TODO: per element subscription removal
            // foreach (var value in _reactiveElements.Values)
            // {
            //     value.Dispose();
            // }
            // _reactiveElements.Clear();
            bufferedValue = ActiveList[0];
            ActiveList.Clear();
            RaiseOnClear();
        }
        
        public bool Remove(T item)
        {
            // var idx = ActiveList.IndexOf(item);
            bufferedValue = item;
            var removed = ActiveList.Remove(item);
            if (removed)
            {
                // TODO: per element subscription removal
                // if (_reactiveElements.ContainsKey(idx))
                //     _reactiveElements.Remove(idx);
                RaiseOnRemove(item);
            }
            return removed;
        }
        
        public void Insert(int index, T item)
        {
            bufferedValue = item;
            ActiveList.Insert(index, item);
            RaiseOnAdd(item);
        }

        public void RemoveAt(int index)
        {
            var item = ActiveList[index];
            bufferedValue = item;
            ActiveList.RemoveAt(index);
            // TODO: per element subscription removal
            // if (_reactiveElements.ContainsKey(index))
            //     _reactiveElements.Remove(index);
            RaiseOnRemove(item);
        }

        public T this[int index]
        {
            get => ActiveList[index];
            set
            {
                bufferedValue = value;
                ActiveList[index] = value;
                // TODO : per element raise
                // if (_reactiveElements.ContainsKey(index)) 
                //     _reactiveElements[index].Value = value;
            }
        }

        public bool Contains(T item) => ActiveList.Contains(item);
        public void CopyTo(T[] array, int arrayIndex) => ActiveList.CopyTo(array, arrayIndex);
        public int Count => ActiveList.Count;
        bool ICollection<T>.IsReadOnly => ActiveList.ToArray().IsReadOnly;
        public int IndexOf(T item) => ActiveList.IndexOf(item);

        // private readonly AsyncReactiveProperty<T> _onAdd = new AsyncReactiveProperty<T>(default);
        // private readonly AsyncReactiveProperty<T> _onRemove = new AsyncReactiveProperty<T>(default);
        // private readonly AsyncReactiveProperty<AsyncUnit> _onClear = new AsyncReactiveProperty<AsyncUnit>(default);
        // private readonly Dictionary<int, AsyncReactiveProperty<T>> _reactiveElements = new Dictionary<int, AsyncReactiveProperty<T>>();
        //
        // private CancellationTokenSource _cts = new CancellationTokenSource();
        //
        // public IUniTaskAsyncEnumerable<T> OnAddAsUniTaskAsyncEnumerable() => _onAdd.WithoutCurrent();
        // public IUniTaskAsyncEnumerable<T> OnRemoveAsUniTaskAsyncEnumerable() => _onRemove.WithoutCurrent();
        // public IUniTaskAsyncEnumerable<AsyncUnit> OnClearAsUniTaskAsyncEnumerable() => _onClear.WithoutCurrent();
        // public IUniTaskAsyncEnumerable<T> ElementAtAsUniTaskAsyncEnumerable(int index, bool skipCurrentValue = false)
        // {
        //     if (!_reactiveElements.ContainsKey(index))
        //     {
        //         var reactive = new AsyncReactiveProperty<T>(this[index]);
        //         _reactiveElements.Add(index, reactive);
        //     }
        //     
        //     if (skipCurrentValue)
        //         return _reactiveElements[index].WithoutCurrent();
        //     return _reactiveElements[index];
        // }

        // public UniTask<T> OnAddAsync(CancellationToken token) => _onAdd.WaitAsync(token);
        // public UniTask<T> OnAddAsync() => OnAddAsync(_cts.Token);
        // public UniTask<T> OnRemoveAsync(CancellationToken token) => _onRemove.WaitAsync(token);
        // public UniTask<T> OnRemoveAsync() => OnRemoveAsync(_cts.Token);
        // public UniTask OnClearAsync(CancellationToken token) => _onClear.WaitAsync(token);
        // public UniTask OnClearAsync() => OnClearAsync(_cts.Token);
        
        // public UniTask<T> ElementAtAsync(int index, CancellationToken token)
        // {
        //     if (!_reactiveElements.ContainsKey(index))
        //     {
        //         var reactive = new AsyncReactiveProperty<T>(this[index]);
        //         _reactiveElements.Add(index, reactive);
        //     }
        //
        //     return _reactiveElements[index].WaitAsync(token);
        // }
        //
        // public UniTask<T> ElementAtAsync(int index) => ElementAtAsync(index, _cts.Token);
        //
        // public IDisposable SubscribeToElementAt(int index, Action<T> action, bool skipCurrentValue = false) => 
        //     ElementAtAsUniTaskAsyncEnumerable(index, skipCurrentValue).Subscribe(action);
        // public IDisposable SubscribeToElementAt(int index, Action<int, T> action, bool skipCurrentValue = false) => 
        //     ElementAtAsUniTaskAsyncEnumerable(index, skipCurrentValue).Subscribe(value => action.Invoke(index, value));
        //
        // public void SubscribeToElementAt(int index, Action<T> action, CancellationToken token, bool skipCurrentValue = false) => 
        //     ElementAtAsUniTaskAsyncEnumerable(index, skipCurrentValue).Subscribe(action, token);
        // public void SubscribeToElementAt(int index, Action<int, T> action, CancellationToken token, bool skipCurrentValue = false) => 
        //     ElementAtAsUniTaskAsyncEnumerable(index, skipCurrentValue).Subscribe(value => action.Invoke(index, value), token);
        
        public override void OnAfterDeserialize()
        {
            _nonSerializedValues.Clear();
            if (_autoResetValues)
                _nonSerializedValues.AddRange(_values);
        }
    }
    
    // public abstract class Collection<TKey, TValue> : ScriptableObject, IDictionary<TKey, TValue>, ISerializationCallbackReceiver, IDisposable
    // {
    //     [SerializeField] private List<SerializedKeyValuePair<TKey, TValue>> _keyValuePairs = new List<SerializedKeyValuePair<TKey, TValue>>();
    //     protected readonly Dictionary<TKey, TValue> activeDictionary = new Dictionary<TKey, TValue>();
    //
    //     private readonly AsyncReactiveProperty<KeyValuePair<TKey, TValue>> _onAdd = new AsyncReactiveProperty<KeyValuePair<TKey, TValue>>(default);
    //     private readonly AsyncReactiveProperty<KeyValuePair<TKey, TValue>> _onRemove = new AsyncReactiveProperty<KeyValuePair<TKey, TValue>>(default);
    //     private readonly AsyncReactiveProperty<AsyncUnit> _onClear = new AsyncReactiveProperty<AsyncUnit>(default);
    //     private readonly Dictionary<TKey, AsyncReactiveProperty<TValue>> _reactiveElements = new Dictionary<TKey, AsyncReactiveProperty<TValue>>();
    //     
    //     private CancellationTokenSource _cts = new CancellationTokenSource();
    //
    //     public IUniTaskAsyncEnumerable<KeyValuePair<TKey, TValue>> OnAddAsUniTaskAsyncEnumerable() => _onAdd.WithoutCurrent();
    //     public IUniTaskAsyncEnumerable<KeyValuePair<TKey, TValue>> OnRemoveAsUniTaskAsyncEnumerable() => _onRemove.WithoutCurrent();
    //     public IUniTaskAsyncEnumerable<AsyncUnit> OnClearAsUniTaskAsyncEnumerable() => _onClear.WithoutCurrent();
    //     public IUniTaskAsyncEnumerable<TValue> ValueAsUniTaskAsyncEnumerable(TKey key, bool skipCurrentValue = false)
    //     {
    //         if (!_reactiveElements.ContainsKey(key))
    //         {
    //             var reactive = new AsyncReactiveProperty<TValue>(this[key]);
    //             _reactiveElements.Add(key, reactive);
    //         }
    //         
    //         if (skipCurrentValue)
    //             return _reactiveElements[key].WithoutCurrent();
    //         return _reactiveElements[key];
    //     }
    //
    //     public void Add(TKey key, TValue value)
    //     {
    //         activeDictionary.Add(key, value);
    //         _onAdd.Value = new KeyValuePair<TKey, TValue>(key, value);
    //     }
    //     
    //     public void Clear()
    //     {
    //         foreach (var value in _reactiveElements.Values)
    //         {
    //             value.Dispose();
    //         }
    //         _reactiveElements.Clear();
    //         activeDictionary.Clear();
    //         _onClear.Value = AsyncUnit.Default;
    //     }
    //     
    //     public bool Remove(TKey key)
    //     {
    //         var value = activeDictionary[key];
    //         var removed = activeDictionary.Remove(key);
    //         _onRemove.Value = new KeyValuePair<TKey, TValue>(key, value);
    //         return removed;
    //     }
    //
    //     public bool TryGetValue(TKey key, out TValue value) => activeDictionary.TryGetValue(key, out value);
    //
    //     public TValue this[TKey key]
    //     {
    //         get => activeDictionary[key];
    //         set
    //         {
    //             activeDictionary[key] = value;
    //             if (_reactiveElements.ContainsKey(key))
    //                 _reactiveElements[key].Value = value;
    //         }
    //     }
    //     
    //     public int Count => activeDictionary.Count;
    //     public bool ContainsKey(TKey key) => activeDictionary.ContainsKey(key);
    //     public bool ContainsValue(TValue value) => activeDictionary.ContainsValue(value);
    //
    //     public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator() => activeDictionary.GetEnumerator();
    //     IEnumerator IEnumerable.GetEnumerator() => activeDictionary.GetEnumerator();
    //
    //     void ICollection<KeyValuePair<TKey, TValue>>.Add(KeyValuePair<TKey, TValue> item) => Add(item.Key, item.Value);
    //     bool ICollection<KeyValuePair<TKey, TValue>>.Contains(KeyValuePair<TKey, TValue> item) => activeDictionary.ContainsKey(item.Key) && activeDictionary.ContainsValue(item.Value);
    //     void ICollection<KeyValuePair<TKey, TValue>>.CopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex) { activeDictionary.ToList().CopyTo(array, arrayIndex); }
    //     bool ICollection<KeyValuePair<TKey, TValue>>.Remove(KeyValuePair<TKey, TValue> item) => Remove(item.Key);
    //     bool ICollection<KeyValuePair<TKey, TValue>>.IsReadOnly => activeDictionary.ToArray().IsReadOnly;
    //
    //     public ICollection<TKey> Keys => activeDictionary.Keys;
    //     public ICollection<TValue> Values => activeDictionary.Values;
    //     
    //     public UniTask<KeyValuePair<TKey, TValue>> OnAddAsync(CancellationToken token) => _onAdd.WaitAsync(token);
    //     public UniTask<KeyValuePair<TKey, TValue>> OnAddAsync() => OnAddAsync(_cts.Token);
    //     public UniTask<KeyValuePair<TKey, TValue>> OnRemoveAsync(CancellationToken token) => _onRemove.WaitAsync(token);
    //     public UniTask<KeyValuePair<TKey, TValue>> OnRemoveAsync() => OnRemoveAsync(_cts.Token);
    //     public UniTask OnClearAsync(CancellationToken token) => _onClear.WaitAsync(token);
    //     public UniTask OnClearAsync() => OnClearAsync(_cts.Token);
    //
    //     public UniTask<TValue> ValueAsync(TKey key, CancellationToken token)
    //     {
    //         if (!_reactiveElements.ContainsKey(key))
    //         {
    //             var reactive = new AsyncReactiveProperty<TValue>(this[key]);
    //             _reactiveElements.Add(key, reactive);
    //         }
    //
    //         return _reactiveElements[key].WaitAsync(token);
    //     }
    //     
    //     public UniTask<TValue> ValueAsync(TKey key) => ValueAsync(key, _cts.Token);
    //
    //     public IDisposable SubscribeOnAdd(Action<KeyValuePair<TKey, TValue>> action) => OnAddAsUniTaskAsyncEnumerable().Subscribe(action);
    //     public IDisposable SubscribeOnRemove(Action<KeyValuePair<TKey, TValue>> action) => OnRemoveAsUniTaskAsyncEnumerable().Subscribe(action);
    //     public IDisposable SubscribeOnClear(Action action) => OnClearAsUniTaskAsyncEnumerable().Subscribe(_ => action.Invoke());
    //     public void SubscribeOnAdd(Action<KeyValuePair<TKey, TValue>> action, CancellationToken token) => OnAddAsUniTaskAsyncEnumerable().Subscribe(action, token);
    //     public void SubscribeOnRemove(Action<KeyValuePair<TKey, TValue>> action, CancellationToken token) => OnRemoveAsUniTaskAsyncEnumerable().Subscribe(action, token);
    //     public void SubscribeOnClear(Action action, CancellationToken token) => OnClearAsUniTaskAsyncEnumerable().Subscribe(_ => action.Invoke(), token);
    //     
    //     public IDisposable SubscribeToValue(TKey key, Action<TValue> action, bool skipCurrentValue = false) => 
    //         ValueAsUniTaskAsyncEnumerable(key, skipCurrentValue).Subscribe(action);
    //     
    //     public IDisposable SubscribeToValue(TKey key, Action<TKey, TValue> action, bool skipCurrentValue = false) => 
    //         ValueAsUniTaskAsyncEnumerable(key, skipCurrentValue).Subscribe(value => action.Invoke(key, value));
    //
    //     public void SubscribeToValue(TKey key, Action<TValue> action, CancellationToken token, bool skipCurrentValue = false) => 
    //         ValueAsUniTaskAsyncEnumerable(key, skipCurrentValue).Subscribe(action, token);
    //
    //     public void SubscribeToValue(TKey key, Action<TKey, TValue> action, CancellationToken token, bool skipCurrentValue = false) => 
    //         ValueAsUniTaskAsyncEnumerable(key, skipCurrentValue).Subscribe(value => action.Invoke(key, value), token);
    //
    //     public void OnAfterDeserialize()
    //     {
    //         Cancel();
    //         
    //         foreach (var value in _reactiveElements.Values) 
    //             value.Dispose();
    //         _reactiveElements.Clear();
    //         
    //         SetActiveDictionary();
    //     }
    //
    //     protected virtual void SetActiveDictionary()
    //     {
    //         activeDictionary.Clear();
    //         foreach (var pair in _keyValuePairs) 
    //             activeDictionary.Add(pair.key, pair.value);
    //     }
    //
    //     public void OnBeforeSerialize() { }
    //
    //     public void Cancel(bool reset = true)
    //     {
    //         try
    //         {
    //             _cts?.Cancel();
    //             _cts?.Dispose();
    //
    //             if (reset)
    //                 _cts = new CancellationTokenSource();
    //         }
    //         catch (Exception)
    //         {
    //             //
    //         }
    //     }
    //
    //     public void Dispose()
    //     {
    //         _onAdd?.Dispose();
    //         _onRemove?.Dispose();
    //         _onClear?.Dispose();
    //         
    //         foreach (var value in _reactiveElements.Values) 
    //             value.Dispose();
    //         
    //         _reactiveElements.Clear();
    //         activeDictionary.Clear();
    //         
    //         Cancel(false);
    //     }
    //
    //     private void OnDestroy() => Dispose();
    // }
    //
    // [Serializable]
    // public class SerializedKeyValuePair<TKey, TValue>
    // {
    //     public TKey key;
    //     public TValue value;
    //
    //     public SerializedKeyValuePair(TKey key, TValue value)
    //     {
    //         this.key = key;
    //         this.value = value;
    //     }
    // }
}