#if KASSETS_UNITASK
using System;
using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using Cysharp.Threading.Tasks.Linq;
using Kadinche.Kassets.Variable;

namespace Kadinche.Kassets.Collection
{
    public abstract partial class Collection<T>
    {
        #region Event Handling
        
        private readonly AsyncReactiveProperty<T> _onAddReactiveProperty = new AsyncReactiveProperty<T>(default);
        private readonly AsyncReactiveProperty<T> _onRemoveReactiveProperty = new AsyncReactiveProperty<T>(default);
        private readonly AsyncReactiveProperty<object> _onClearReactiveProperty = new AsyncReactiveProperty<object>(default);
        private readonly IDictionary<int, AsyncReactiveProperty<T>> _valueReactiveProperties = new Dictionary<int, AsyncReactiveProperty<T>>();

        public IUniTaskAsyncEnumerable<T> OnAddAsyncEnumerable() => _onAddReactiveProperty;
        public IUniTaskAsyncEnumerable<T> OnRemoveAsyncEnumerable() => _onRemoveReactiveProperty;
        public IUniTaskAsyncEnumerable<object> OnClearAsyncEnumerable() => _onClearReactiveProperty;

        public IUniTaskAsyncEnumerable<T> ValueAtAsyncEnumerable(int index)
        {
            if (!_valueReactiveProperties.TryGetValue(index, out var elementSubject))
            {
                elementSubject = new AsyncReactiveProperty<T>(default);
                _valueReactiveProperties.Add(index, elementSubject);
            }

            return elementSubject;
        }
        
        public IDisposable SubscribeOnAdd(Action<T> action)
        {
            return _onAddReactiveProperty.Subscribe(action);
        }

        public void SubscribeOnAdd(Action<T> action, CancellationToken cancellationToken)
        {
            _onAddReactiveProperty.Subscribe(action, cancellationToken);
        }

        public IDisposable SubscribeOnRemove(Action<T> action)
        {
            return _onRemoveReactiveProperty.Subscribe(action);
        }

        public void SubscribeOnRemove(Action<T> action, CancellationToken cancellationToken)
        {
            _onRemoveReactiveProperty.Subscribe(action, cancellationToken);
        }

        public IDisposable SubscribeOnClear(Action action)
        {
            return _onClearReactiveProperty.Subscribe(_ => action.Invoke());
        }

        public void SubscribeOnClear(Action action, CancellationToken cancellationToken)
        {
            _onClearReactiveProperty.Subscribe(_ => action.Invoke(), cancellationToken);
        }

        public IDisposable SubscribeToValueAt(int index, Action<T> action)
        {
            return ValueAtAsyncEnumerable(index).Subscribe(action);
        }

        public void SubscribeToValueAt(int index, Action<T> action, CancellationToken cancellationToken)
        {
            ValueAtAsyncEnumerable(index).Subscribe(action, cancellationToken);
        }
        
        private void RaiseOnAdd(T addedValue) => _onAddReactiveProperty.Value = addedValue;
        private void RaiseOnRemove(T removedValue) => _onRemoveReactiveProperty.Value = removedValue;
        private void RaiseOnClear() => _onClearReactiveProperty.Value = this;
        private void RaiseValueAt(int index, T value)
        {
            if (_variableEventType == VariableEventType.ValueChange && _value[index].Equals(value))
                return;

            if (_valueReactiveProperties.TryGetValue(index, out var reactiveProperty))
            {
                reactiveProperty.Value = value;
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
            ClearValueSubscriptions();
        }
        
        #endregion
    }

    public abstract partial class Collection<TKey, TValue>
    {
        #region Event Handling
        
        private readonly IDictionary<TKey, AsyncReactiveProperty<TValue>> _valueReactiveProperties = new Dictionary<TKey, AsyncReactiveProperty<TValue>>();
        
        public IUniTaskAsyncEnumerable<TValue> ValueAtAsyncEnumerable(TKey key)
        {
            if (!_valueReactiveProperties.TryGetValue(key, out var reactiveProperty))
            {
                reactiveProperty = new AsyncReactiveProperty<TValue>(default);
                _valueReactiveProperties.Add(key, reactiveProperty);
            }

            return reactiveProperty;
        }
        
        public IDisposable SubscribeOnAdd(Action<TKey, TValue> action) => SubscribeOnAdd(pair => action.Invoke(pair.key, pair.value));
        public void SubscribeOnAdd(Action<TKey, TValue> action, CancellationToken cancellationToken) => SubscribeOnAdd(pair => action.Invoke(pair.key, pair.value), cancellationToken);

        public IDisposable SubscribeOnRemove(Action<TKey, TValue> action) => SubscribeOnRemove(pair => action.Invoke(pair.key, pair.value));
        public void SubscribeOnRemove(Action<TKey, TValue> action, CancellationToken cancellationToken) => SubscribeOnRemove(pair => action.Invoke(pair.key, pair.value), cancellationToken);
        
        public IDisposable SubscribeToValue(TKey key, Action<TValue> action)
        {
            return ValueAtAsyncEnumerable(key).Subscribe(action);
        }

        public void SubscribeToValue(TKey key, Action<TValue> action, CancellationToken cancellationToken)
        {
            ValueAtAsyncEnumerable(key).Subscribe(action, cancellationToken);
        }
        
        private void RaiseValue(TKey key, TValue value)
        {
            if (_variableEventType == VariableEventType.ValueChange && _activeDictionary[key].Equals(value))
                return;
            
            if (_valueReactiveProperties.TryGetValue(key, out var reactiveProperty))
            {
                reactiveProperty.Value = value;
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
        
        private void RemoveValueSubscription(TKey key)
        {
            if (_valueReactiveProperties.TryGetValue(key, out var asyncReactiveProperty))
            {
                asyncReactiveProperty.Dispose();
                _valueReactiveProperties.Remove(key);
            }
        }

        #endregion
    }
}

#endif