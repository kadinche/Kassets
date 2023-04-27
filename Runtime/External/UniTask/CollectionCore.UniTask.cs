#if KASSETS_UNITASK
using System;
using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using Cysharp.Threading.Tasks.Linq;

namespace Kadinche.Kassets.Collection
{
    public abstract partial class Collection<T>
    {
        #region Event Handling

        private readonly AsyncReactiveProperty<T> _onAddReactiveProperty = new AsyncReactiveProperty<T>(default);
        private readonly AsyncReactiveProperty<T> _onRemoveReactiveProperty = new AsyncReactiveProperty<T>(default);
        private readonly AsyncReactiveProperty<object> _onClearReactiveProperty = new AsyncReactiveProperty<object>(default);
        private readonly AsyncReactiveProperty<int> _countReactiveProperty = new AsyncReactiveProperty<int>(default);

        private readonly IDictionary<int, AsyncReactiveProperty<T>> _valueReactiveProperties =
            new Dictionary<int, AsyncReactiveProperty<T>>();

        public IUniTaskAsyncEnumerable<T> OnAddAsyncEnumerable() => _onAddReactiveProperty;
        public IUniTaskAsyncEnumerable<T> OnRemoveAsyncEnumerable() => _onRemoveReactiveProperty;
        public IUniTaskAsyncEnumerable<object> OnClearAsyncEnumerable() => _onClearReactiveProperty;
        public IUniTaskAsyncEnumerable<int> CountAsyncEnumerable() => _countReactiveProperty;

        public IAsyncReactiveProperty<T> ValueAtAsyncEnumerable(int index)
        {
            if (!_valueReactiveProperties.TryGetValue(index, out var elementSubject))
            {
                elementSubject = new AsyncReactiveProperty<T>(default);
                _valueReactiveProperties.Add(index, elementSubject);
            }

            return elementSubject;
        }

        public void SubscribeOnAdd(Action<T> action, CancellationToken cancellationToken)
        {
            var token = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken, cts.Token).Token;
            _onAddReactiveProperty.Subscribe(action, token);
        }
        
        public void SubscribeOnRemove(Action<T> action, CancellationToken cancellationToken)
        {
            var token = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken, cts.Token).Token;
            _onRemoveReactiveProperty.Subscribe(action, token);
        }
        
        public void SubscribeOnClear(Action action, CancellationToken cancellationToken)
        {
            var token = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken, cts.Token).Token;
            _onClearReactiveProperty.Subscribe(_ => action.Invoke(), token);
        }
        
        public void SubscribeToCount(Action<int> action, CancellationToken cancellationToken)
        {
            var token = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken, cts.Token).Token;
            _countReactiveProperty.Subscribe(action, token);
        }

        public void SubscribeToValueAt(int index, Action<T> action, CancellationToken cancellationToken)
        {
            var token = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken, cts.Token).Token;
            ValueAtAsyncEnumerable(index).Subscribe(action, token);
        }
        
        #endregion
    }

    public abstract partial class Collection<TKey, TValue>
    {
        #region Event Handling
        
        private readonly IDictionary<TKey, AsyncReactiveProperty<TValue>> _valueReactiveProperties = new Dictionary<TKey, AsyncReactiveProperty<TValue>>();
        
        public IAsyncReactiveProperty<TValue> ValueAtAsyncEnumerable(TKey key)
        {
            if (!_valueReactiveProperties.TryGetValue(key, out var reactiveProperty))
            {
                reactiveProperty = new AsyncReactiveProperty<TValue>(default);
                _valueReactiveProperties.Add(key, reactiveProperty);
            }

            return reactiveProperty;
        }
        
        public void SubscribeOnAdd(Action<TKey, TValue> action, CancellationToken cancellationToken) => SubscribeOnAdd(pair => action.Invoke(pair.key, pair.value), cancellationToken);

        public void SubscribeOnRemove(Action<TKey, TValue> action, CancellationToken cancellationToken) => SubscribeOnRemove(pair => action.Invoke(pair.key, pair.value), cancellationToken);

        public void SubscribeToValue(TKey key, Action<TValue> action, CancellationToken cancellationToken)
        {
            var token = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken, cts.Token).Token;
            ValueAtAsyncEnumerable(key).Subscribe(action, token);
        }

        #endregion
    }
    
#if KASSETS_UNIRX
    public abstract partial class Collection<T>
    {
        private void RaiseOnAdd_UniTask(T addedValue) => _onAddReactiveProperty.Value = addedValue;
        private void RaiseOnRemove_UniTask(T removedValue) => _onRemoveReactiveProperty.Value = removedValue;
        private void RaiseOnClear_UniTask() => _onClearReactiveProperty.Value = this;
        private void RaiseCount_UniTask() => _countReactiveProperty.Value = Count;
        private void RaiseValueAt_UniTask(int index, T value)
        {
            if (variableEventType == VariableEventType.ValueChange && _value[index].Equals(value))
                return;

            if (_valueReactiveProperties.TryGetValue(index, out var reactiveProperty))
            {
                reactiveProperty.Value = value;
            }
        }
        
        protected IDisposable SubscribeOnAdd_UniTask(Action<T> action)
        {
            return _onAddReactiveProperty.Subscribe(action);
        }

        protected IDisposable SubscribeOnRemove_UniTask(Action<T> action)
        {
            return _onRemoveReactiveProperty.Subscribe(action);
        }
        
        private IDisposable SubscribeOnClear_UniTask(Action action)
        {
            return _onClearReactiveProperty.Subscribe(_ => action.Invoke());
        }
        
        private IDisposable SubscribeToCount_UniTask(Action<int> action)
        {
            return _countReactiveProperty.Subscribe(action);
        }
        
        private IDisposable SubscribeToValueAt_UniTask(int index, Action<T> action)
        {
            return ValueAtAsyncEnumerable(index).Subscribe(action);
        }
        
        private void IncrementValueSubscriptions_UniTask(int index)
        {
            for (var i = _value.Count; i > index; i--)
            {
                _valueReactiveProperties.TryChangeKey(i - 1, i);
            }
        }
        
        private void ClearValueSubscriptions_UniTask()
        {
            foreach (var reactiveProperty in _valueReactiveProperties.Values)
            {
                reactiveProperty.Dispose();
            }
            _valueReactiveProperties.Clear();
        }
        
        private void RemoveValueSubscription_UniTask(int index)
        {
            if (_valueReactiveProperties.TryGetValue(index, out var reactiveProperty))
            {
                reactiveProperty.Dispose();
                _valueReactiveProperties.Remove(index);
            }
        }

        private void DisposeSubscriptions_UniTask()
        {
            _onAddReactiveProperty.Dispose();
            _onRemoveReactiveProperty.Dispose();
            _onClearReactiveProperty.Dispose();
            _countReactiveProperty.Dispose();
            ClearValueSubscriptions_UniTask();
        }
    }

    public abstract partial class Collection<TKey, TValue>
    {
        private void RaiseValue_UniTask(TKey key, TValue value)
        {
            if (variableEventType == VariableEventType.ValueChange && _activeDictionary[key].Equals(value))
                return;
            
            if (_valueReactiveProperties.TryGetValue(key, out var reactiveProperty))
            {
                reactiveProperty.Value = value;
            }
        }
        
        public IDisposable SubscribeOnAdd(Action<TKey, TValue> action) => SubscribeOnAdd_UniTask(pair => action.Invoke(pair.key, pair.value));
        public IDisposable SubscribeOnRemove(Action<TKey, TValue> action) => SubscribeOnRemove_UniTask(pair => action.Invoke(pair.key, pair.value));
        public IDisposable SubscribeToValue_UniTask(TKey key, Action<TValue> action)
        {
            return ValueAtAsyncEnumerable(key).Subscribe(action);
        }
        
        private void ClearValueSubscriptions_UniTask()
        {
            foreach (var reactiveProperty in _valueReactiveProperties.Values)
            {
                reactiveProperty.Dispose();
            }
            _valueReactiveProperties.Clear();
        }
        
        private void RemoveValueSubscription_UniTask(TKey key)
        {
            if (_valueReactiveProperties.TryGetValue(key, out var asyncReactiveProperty))
            {
                asyncReactiveProperty.Dispose();
                _valueReactiveProperties.Remove(key);
            }
        }
    }
#else
    public abstract partial class Collection<T>
    {
        private void RaiseOnAdd(T addedValue) => _onAddReactiveProperty.Value = addedValue;
        private void RaiseOnRemove(T removedValue) => _onRemoveReactiveProperty.Value = removedValue;
        private void RaiseOnClear() => _onClearReactiveProperty.Value = this;
        private void RaiseCount() => _countReactiveProperty.Value = Count;
        private void RaiseValueAt(int index, T value)
        {
            if (variableEventType == VariableEventType.ValueChange && _value[index].Equals(value))
                return;

            if (_valueReactiveProperties.TryGetValue(index, out var reactiveProperty))
            {
                reactiveProperty.Value = value;
            }
        }

        public IDisposable SubscribeOnAdd(Action<T> action)
        {
            return _onAddReactiveProperty.Subscribe(action);
        }
        
        public IDisposable SubscribeOnRemove(Action<T> action)
        {
            return _onRemoveReactiveProperty.Subscribe(action);
        }
        
        public IDisposable SubscribeOnClear(Action action)
        {
            return _onClearReactiveProperty.Subscribe(_ => action.Invoke());
        }
        
        public IDisposable SubscribeToCount(Action<int> action)
        {
            return _countReactiveProperty.Subscribe(action);
        }
        
        public IDisposable SubscribeToValueAt(int index, Action<T> action)
        {
            return ValueAtAsyncEnumerable(index).Subscribe(action);
        }
        
        private void IncrementValueSubscriptions(int index)
        {
            for (var i = _value.Count; i > index; i--)
            {
                _valueReactiveProperties.TryChangeKey(i - 1, i);
            }
        }
        
        private void ClearValueSubscriptions()
        {
            foreach (var reactiveProperty in _valueReactiveProperties.Values)
            {
                reactiveProperty.Dispose();
            }
            _valueReactiveProperties.Clear();
        }
        
        private void RemoveValueSubscription(int index)
        {
            if (_valueReactiveProperties.TryGetValue(index, out var reactiveProperty))
            {
                reactiveProperty.Dispose();
                _valueReactiveProperties.Remove(index);
            }
        }

        private void DisposeSubscriptions()
        {
            _onAddReactiveProperty.Dispose();
            _onRemoveReactiveProperty.Dispose();
            _onClearReactiveProperty.Dispose();
            _countReactiveProperty.Dispose();
            ClearValueSubscriptions();
        }
    }

    public abstract partial class Collection<TKey, TValue>
    {
        private void RaiseValue(TKey key, TValue value)
        {
            if (variableEventType == VariableEventType.ValueChange && _activeDictionary[key].Equals(value))
                return;
            
            if (_valueReactiveProperties.TryGetValue(key, out var reactiveProperty))
            {
                reactiveProperty.Value = value;
            }
        }
        
        public IDisposable SubscribeOnAdd(Action<TKey, TValue> action) => SubscribeOnAdd(pair => action.Invoke(pair.key, pair.value));
        public IDisposable SubscribeOnRemove(Action<TKey, TValue> action) => SubscribeOnRemove(pair => action.Invoke(pair.key, pair.value));
        public IDisposable SubscribeToValue(TKey key, Action<TValue> action)
        {
            return ValueAtAsyncEnumerable(key).Subscribe(action);
        }

        private void ClearValueSubscriptions()
        {
            foreach (var reactiveProperty in _valueReactiveProperties.Values)
            {
                reactiveProperty.Dispose();
            }
            _valueReactiveProperties.Clear();
        }
        
        private void RemoveValueSubscription(TKey key)
        {
            if (_valueReactiveProperties.TryGetValue(key, out var asyncReactiveProperty))
            {
                asyncReactiveProperty.Dispose();
                _valueReactiveProperties.Remove(key);
            }
        }
    }
#endif
    
}

#endif