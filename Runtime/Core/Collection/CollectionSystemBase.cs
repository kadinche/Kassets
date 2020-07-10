using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Cysharp.Threading.Tasks;
using Cysharp.Threading.Tasks.Linq;
using UnityEngine;

#if ODIN_INSPECTOR
using Sirenix.OdinInspector;
#endif

namespace Kassets.Collection
{
    
    #if ODIN_INSPECTOR
    [InlineEditor()]
    #endif
    public abstract class Collection<T> : ScriptableObject, IList<T>, IDisposable, ISerializationCallbackReceiver
    {
        [SerializeField] private List<T> _values = new List<T>();
        private readonly List<T> _nonSerializedValues = new List<T>();

        [Tooltip("If true will serialize any changes to collection contents when play mode end.")]
        [SerializeField] private bool _keep;
        
        private List<T> ActiveList => _keep ? _values : _nonSerializedValues;

        private readonly AsyncReactiveProperty<T> _onAdd = new AsyncReactiveProperty<T>(default);
        private readonly AsyncReactiveProperty<T> _onRemove = new AsyncReactiveProperty<T>(default);
        private readonly AsyncReactiveProperty<AsyncUnit> _onClear = new AsyncReactiveProperty<AsyncUnit>(default);
        private readonly Dictionary<int, AsyncReactiveProperty<T>> _reactiveElements = new Dictionary<int, AsyncReactiveProperty<T>>();
        
        private CancellationTokenSource _cts = new CancellationTokenSource();

        public IUniTaskAsyncEnumerable<T> OnAddAsUniTaskAsyncEnumerable() => _onAdd.WithoutCurrent();
        public IUniTaskAsyncEnumerable<T> OnRemoveAsUniTaskAsyncEnumerable() => _onRemove.WithoutCurrent();
        public IUniTaskAsyncEnumerable<AsyncUnit> OnClearAsUniTaskAsyncEnumerable() => _onClear.WithoutCurrent();
        public IUniTaskAsyncEnumerable<T> ElementAtAsUniTaskAsyncEnumerable(int index, bool skipCurrentValue = false)
        {
            if (!_reactiveElements.ContainsKey(index))
            {
                var reactive = new AsyncReactiveProperty<T>(this[index]);
                _reactiveElements.Add(index, reactive);
            }
            
            if (skipCurrentValue)
                return _reactiveElements[index].WithoutCurrent();
            return _reactiveElements[index];
        }

        public IEnumerator<T> GetEnumerator() => ActiveList.GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => ActiveList.GetEnumerator();

        public void Add(T item)
        {
            ActiveList.Add(item);
            _onAdd.Value = item;
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
            foreach (var value in _reactiveElements.Values)
            {
                value.Dispose();
            }
            _reactiveElements.Clear();
            ActiveList.Clear();
            _onClear.Value = AsyncUnit.Default;
        }
        
        public bool Remove(T item)
        {
            var idx = ActiveList.IndexOf(item);
            var removed = ActiveList.Remove(item);
            if (removed)
            {
                if (_reactiveElements.ContainsKey(idx))
                    _reactiveElements.Remove(idx);
                _onRemove.Value = item;
            }
            return removed;
        }
        
        public void Insert(int index, T item)
        {
            ActiveList.Insert(index, item);
            _onAdd.Value = item;
        }

        public void RemoveAt(int index)
        {
            var item = ActiveList[index];
            ActiveList.RemoveAt(index);
            if (_reactiveElements.ContainsKey(index))
                _reactiveElements.Remove(index);
            _onRemove.Value = item;
        }

        public T this[int index]
        {
            get => ActiveList[index];
            set
            {
                ActiveList[index] = value;
                if (_reactiveElements.ContainsKey(index)) 
                    _reactiveElements[index].Value = value;
            }
        }

        public bool Contains(T item) => ActiveList.Contains(item);
        public void CopyTo(T[] array, int arrayIndex) => ActiveList.CopyTo(array, arrayIndex);
        public int Count => ActiveList.Count;
        bool ICollection<T>.IsReadOnly => ActiveList.ToArray().IsReadOnly;
        public int IndexOf(T item) => ActiveList.IndexOf(item);
        
        public UniTask<T> OnAddAsync(CancellationToken token) => _onAdd.WaitAsync(token);
        public UniTask<T> OnAddAsync() => OnAddAsync(_cts.Token);
        public UniTask<T> OnRemoveAsync(CancellationToken token) => _onRemove.WaitAsync(token);
        public UniTask<T> OnRemoveAsync() => OnRemoveAsync(_cts.Token);
        public UniTask OnClearAsync(CancellationToken token) => _onClear.WaitAsync(token);
        public UniTask OnClearAsync() => OnClearAsync(_cts.Token);
        
        public UniTask<T> ElementAtAsync(int index, CancellationToken token)
        {
            if (!_reactiveElements.ContainsKey(index))
            {
                var reactive = new AsyncReactiveProperty<T>(this[index]);
                _reactiveElements.Add(index, reactive);
            }

            return _reactiveElements[index].WaitAsync(token);
        }
        
        public UniTask<T> ElementAtAsync(int index) => ElementAtAsync(index, _cts.Token);

        public IDisposable SubscribeToElementAt(int index, Action<T> action, bool skipCurrentValue = false) => 
            ElementAtAsUniTaskAsyncEnumerable(index, skipCurrentValue).Subscribe(action);
        public IDisposable SubscribeToElementAt(int index, Action<int, T> action, bool skipCurrentValue = false) => 
            ElementAtAsUniTaskAsyncEnumerable(index, skipCurrentValue).Subscribe(value => action.Invoke(index, value));

        public void SubscribeToElementAt(int index, Action<T> action, CancellationToken token, bool skipCurrentValue = false) => 
            ElementAtAsUniTaskAsyncEnumerable(index, skipCurrentValue).Subscribe(action, token);
        public void SubscribeToElementAt(int index, Action<int, T> action, CancellationToken token, bool skipCurrentValue = false) => 
            ElementAtAsUniTaskAsyncEnumerable(index, skipCurrentValue).Subscribe(value => action.Invoke(index, value), token);

        public IDisposable SubscribeOnAdd(Action<T> action) => OnAddAsUniTaskAsyncEnumerable().Subscribe(action);
        public IDisposable SubscribeOnRemove(Action<T> action) => OnRemoveAsUniTaskAsyncEnumerable().Subscribe(action);
        public IDisposable SubscribeOnClear(Action action) => OnClearAsUniTaskAsyncEnumerable().Subscribe(_ => action.Invoke());
        public void SubscribeOnAdd(Action<T> action, CancellationToken token) => OnAddAsUniTaskAsyncEnumerable().Subscribe(action, token);
        public void SubscribeOnRemove(Action<T> action, CancellationToken token) => OnRemoveAsUniTaskAsyncEnumerable().Subscribe(action, token);
        public void SubscribeOnClear(Action action, CancellationToken token) => OnClearAsUniTaskAsyncEnumerable().Subscribe(_ => action.Invoke(), token);

        public void OnAfterDeserialize()
        {
            _nonSerializedValues.Clear();
            if (!_keep)
                _nonSerializedValues.AddRange(_values);
            Cancel();
        }
        
        public void OnBeforeSerialize()
        {
        }

        public void Cancel(bool reset = true)
        {
            try
            {
                _cts?.Cancel();
                _cts?.Dispose();

                if (reset)
                    _cts = new CancellationTokenSource();
            }
            catch (Exception)
            {
                //
            }
        }

        public void Dispose()
        {
            _onAdd?.Dispose();
            _onRemove?.Dispose();
            _onClear?.Dispose();
            
            Cancel(false);
        }

        private void OnDestroy() => Dispose();
    }
    
    #if ODIN_INSPECTOR
    [InlineEditor()]
    #endif
    public abstract class Collection<TKey, TValue> : ScriptableObject, IDictionary<TKey, TValue>, ISerializationCallbackReceiver, IDisposable
    {
        [SerializeField] private List<SerializedKeyValuePair<TKey, TValue>> _keyValuePairs = new List<SerializedKeyValuePair<TKey, TValue>>();
        protected readonly Dictionary<TKey, TValue> activeDictionary = new Dictionary<TKey, TValue>();

        private readonly AsyncReactiveProperty<KeyValuePair<TKey, TValue>> _onAdd = new AsyncReactiveProperty<KeyValuePair<TKey, TValue>>(default);
        private readonly AsyncReactiveProperty<KeyValuePair<TKey, TValue>> _onRemove = new AsyncReactiveProperty<KeyValuePair<TKey, TValue>>(default);
        private readonly AsyncReactiveProperty<AsyncUnit> _onClear = new AsyncReactiveProperty<AsyncUnit>(default);
        private readonly Dictionary<TKey, AsyncReactiveProperty<TValue>> _reactiveElements = new Dictionary<TKey, AsyncReactiveProperty<TValue>>();
        
        private CancellationTokenSource _cts = new CancellationTokenSource();

        public IUniTaskAsyncEnumerable<KeyValuePair<TKey, TValue>> OnAddAsUniTaskAsyncEnumerable() => _onAdd.WithoutCurrent();
        public IUniTaskAsyncEnumerable<KeyValuePair<TKey, TValue>> OnRemoveAsUniTaskAsyncEnumerable() => _onRemove.WithoutCurrent();
        public IUniTaskAsyncEnumerable<AsyncUnit> OnClearAsUniTaskAsyncEnumerable() => _onClear.WithoutCurrent();
        public IUniTaskAsyncEnumerable<TValue> ValueAsUniTaskAsyncEnumerable(TKey key, bool skipCurrentValue = false)
        {
            if (!_reactiveElements.ContainsKey(key))
            {
                var reactive = new AsyncReactiveProperty<TValue>(this[key]);
                _reactiveElements.Add(key, reactive);
            }
            
            if (skipCurrentValue)
                return _reactiveElements[key].WithoutCurrent();
            return _reactiveElements[key];
        }

        public void Add(TKey key, TValue value)
        {
            activeDictionary.Add(key, value);
            _onAdd.Value = new KeyValuePair<TKey, TValue>(key, value);
        }
        
        public void Clear()
        {
            foreach (var value in _reactiveElements.Values)
            {
                value.Dispose();
            }
            _reactiveElements.Clear();
            activeDictionary.Clear();
            _onClear.Value = AsyncUnit.Default;
        }
        
        public bool Remove(TKey key)
        {
            var value = activeDictionary[key];
            var removed = activeDictionary.Remove(key);
            _onRemove.Value = new KeyValuePair<TKey, TValue>(key, value);
            return removed;
        }

        public bool TryGetValue(TKey key, out TValue value) => activeDictionary.TryGetValue(key, out value);

        public TValue this[TKey key]
        {
            get => activeDictionary[key];
            set
            {
                activeDictionary[key] = value;
                if (_reactiveElements.ContainsKey(key))
                    _reactiveElements[key].Value = value;
            }
        }
        
        public int Count => activeDictionary.Count;
        public bool ContainsKey(TKey key) => activeDictionary.ContainsKey(key);
        public bool ContainsValue(TValue value) => activeDictionary.ContainsValue(value);

        public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator() => activeDictionary.GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => activeDictionary.GetEnumerator();

        void ICollection<KeyValuePair<TKey, TValue>>.Add(KeyValuePair<TKey, TValue> item) => Add(item.Key, item.Value);
        bool ICollection<KeyValuePair<TKey, TValue>>.Contains(KeyValuePair<TKey, TValue> item) => activeDictionary.ContainsKey(item.Key) && activeDictionary.ContainsValue(item.Value);
        void ICollection<KeyValuePair<TKey, TValue>>.CopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex) { activeDictionary.ToList().CopyTo(array, arrayIndex); }
        bool ICollection<KeyValuePair<TKey, TValue>>.Remove(KeyValuePair<TKey, TValue> item) => Remove(item.Key);
        bool ICollection<KeyValuePair<TKey, TValue>>.IsReadOnly => activeDictionary.ToArray().IsReadOnly;

        public ICollection<TKey> Keys => activeDictionary.Keys;
        public ICollection<TValue> Values => activeDictionary.Values;
        
        public UniTask<KeyValuePair<TKey, TValue>> OnAddAsync(CancellationToken token) => _onAdd.WaitAsync(token);
        public UniTask<KeyValuePair<TKey, TValue>> OnAddAsync() => OnAddAsync(_cts.Token);
        public UniTask<KeyValuePair<TKey, TValue>> OnRemoveAsync(CancellationToken token) => _onRemove.WaitAsync(token);
        public UniTask<KeyValuePair<TKey, TValue>> OnRemoveAsync() => OnRemoveAsync(_cts.Token);
        public UniTask OnClearAsync(CancellationToken token) => _onClear.WaitAsync(token);
        public UniTask OnClearAsync() => OnClearAsync(_cts.Token);

        public UniTask<TValue> ValueAsync(TKey key, CancellationToken token)
        {
            if (!_reactiveElements.ContainsKey(key))
            {
                var reactive = new AsyncReactiveProperty<TValue>(this[key]);
                _reactiveElements.Add(key, reactive);
            }

            return _reactiveElements[key].WaitAsync(token);
        }
        
        public UniTask<TValue> ValueAsync(TKey key) => ValueAsync(key, _cts.Token);

        public IDisposable SubscribeOnAdd(Action<KeyValuePair<TKey, TValue>> action) => OnAddAsUniTaskAsyncEnumerable().Subscribe(action);
        public IDisposable SubscribeOnRemove(Action<KeyValuePair<TKey, TValue>> action) => OnRemoveAsUniTaskAsyncEnumerable().Subscribe(action);
        public IDisposable SubscribeOnClear(Action action) => OnClearAsUniTaskAsyncEnumerable().Subscribe(_ => action.Invoke());
        public void SubscribeOnAdd(Action<KeyValuePair<TKey, TValue>> action, CancellationToken token) => OnAddAsUniTaskAsyncEnumerable().Subscribe(action, token);
        public void SubscribeOnRemove(Action<KeyValuePair<TKey, TValue>> action, CancellationToken token) => OnRemoveAsUniTaskAsyncEnumerable().Subscribe(action, token);
        public void SubscribeOnClear(Action action, CancellationToken token) => OnClearAsUniTaskAsyncEnumerable().Subscribe(_ => action.Invoke(), token);
        
        public IDisposable SubscribeToValue(TKey key, Action<TValue> action, bool skipCurrentValue = false) => 
            ValueAsUniTaskAsyncEnumerable(key, skipCurrentValue).Subscribe(action);
        
        public IDisposable SubscribeToValue(TKey key, Action<TKey, TValue> action, bool skipCurrentValue = false) => 
            ValueAsUniTaskAsyncEnumerable(key, skipCurrentValue).Subscribe(value => action.Invoke(key, value));

        public void SubscribeToValue(TKey key, Action<TValue> action, CancellationToken token, bool skipCurrentValue = false) => 
            ValueAsUniTaskAsyncEnumerable(key, skipCurrentValue).Subscribe(action, token);

        public void SubscribeToValue(TKey key, Action<TKey, TValue> action, CancellationToken token, bool skipCurrentValue = false) => 
            ValueAsUniTaskAsyncEnumerable(key, skipCurrentValue).Subscribe(value => action.Invoke(key, value), token);

        public void OnAfterDeserialize()
        {
            Cancel();
            
            foreach (var value in _reactiveElements.Values) 
                value.Dispose();
            _reactiveElements.Clear();
            
            SetActiveDictionary();
        }

        protected virtual void SetActiveDictionary()
        {
            activeDictionary.Clear();
            foreach (var pair in _keyValuePairs) 
                activeDictionary.Add(pair.key, pair.value);
        }

        public void OnBeforeSerialize() { }

        public void Cancel(bool reset = true)
        {
            try
            {
                _cts?.Cancel();
                _cts?.Dispose();

                if (reset)
                    _cts = new CancellationTokenSource();
            }
            catch (Exception)
            {
                //
            }
        }

        public void Dispose()
        {
            _onAdd?.Dispose();
            _onRemove?.Dispose();
            _onClear?.Dispose();
            
            foreach (var value in _reactiveElements.Values) 
                value.Dispose();
            
            _reactiveElements.Clear();
            activeDictionary.Clear();
            
            Cancel(false);
        }

        private void OnDestroy() => Dispose();
    }
    
    [Serializable]
    public class SerializedKeyValuePair<TKey, TValue>
    {
        public TKey key;
        public TValue value;

        public SerializedKeyValuePair(TKey key, TValue value)
        {
            this.key = key;
            this.value = value;
        }
    }
}