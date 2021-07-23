#if KASSETS_UNIRX
using System;
using System.Collections.Generic;
using System.Linq;
using Kadinche.Kassets.Variable;
using UniRx;

namespace Kadinche.Kassets.Collection
{
    public abstract partial class Collection<T>
    {
        #region Event Handling
        
        private readonly Subject<T> _onAddSubject = new Subject<T>();
        private readonly Subject<T> _onRemoveSubject = new Subject<T>();
        private readonly Subject<object> _onClearSubject = new Subject<object>();
        private readonly IDictionary<int, Subject<T>> _valueSubjects = new Dictionary<int, Subject<T>>();

        public IObservable<T> OnAddObservable() => _onAddSubject;
        public IObservable<T> OnRemoveObservable() => _onRemoveSubject;
        public IObservable<object> OnClearObservable() => _onClearSubject;

        public IObservable<T> ValueAtObservable(int index)
        {
            if (!_valueSubjects.TryGetValue(index, out var elementSubject))
            {
                elementSubject = new Subject<T>();
                _valueSubjects.Add(index, elementSubject);
            }

            return elementSubject;
        }
        
        public IDisposable SubscribeOnAdd(Action<T> action, bool withBuffer)
        {
            var subscription = _onAddSubject.Subscribe(action);
            
            if (withBuffer)
            {
                action.Invoke(_value.LastOrDefault());
            }

            return subscription;
        }

        public IDisposable SubscribeOnRemove(Action<T> action, bool withBuffer)
        {
            var subscription = _onRemoveSubject.Subscribe(action);
            
            if (withBuffer)
            {
                action.Invoke(_lastRemoved);
            }

            return subscription;
        }

        public IDisposable SubscribeOnClear(Action action, bool withBuffer)
        {
            var subscription = _onClearSubject.Subscribe(_ => action.Invoke());
            
            if (withBuffer)
            {
                action.Invoke();
            }

            return subscription;
        }

        public IDisposable SubscribeToValueAt(int index, Action<T> action, bool withBuffer)
        {
            var subscription = ValueAtObservable(index).Subscribe(action);

            if (_value[index] != null && withBuffer)
            {
                action.Invoke(_value[index]);
            }

            return subscription;
        }
        
        private void RaiseOnAdd(T addedValue) => _onAddSubject.OnNext(addedValue);
        private void RaiseOnRemove(T removedValue) => _onRemoveSubject.OnNext(removedValue);
        private void RaiseOnClear() => _onClearSubject.OnNext(this);
        private void RaiseValueAt(int index, T value)
        {
            if (_variableEventType == VariableEventType.ValueChange && _value[index].Equals(value))
                return;

            if (_valueSubjects.TryGetValue(index, out var subject))
            {
                subject.OnNext(value);
            }
        }
        
        private void ClearValueSubscriptions()
        {
            foreach (var subject in _valueSubjects.Values)
            {
                subject.Dispose();
            }
            _valueSubjects.Clear();
        }
        
        private void RemoveValueSubscription(int index)
        {
            if (_valueSubjects.TryGetValue(index, out var subject))
            {
                subject.Dispose();
                _valueSubjects.Remove(index);
            }
        }

        private void DisposeSubscriptions()
        {
            _onAddSubject.Dispose();
            _onRemoveSubject.Dispose();
            _onClearSubject.Dispose();
            ClearValueSubscriptions();
        }
        
        #endregion
    }

    public abstract partial class Collection<TKey, TValue>
    {
        #region Event Handling
        
        private readonly IDictionary<TKey, Subject<TValue>> _valueSubjects = new Dictionary<TKey, Subject<TValue>>();
        
        public IObservable<TValue> ValueAtObservable(TKey key)
        {
            if (!_valueSubjects.TryGetValue(key, out var elementSubject))
            {
                elementSubject = new Subject<TValue>();
                _valueSubjects.Add(key, elementSubject);
            }

            return elementSubject;
        }
        
        public IDisposable SubscribeToValue(TKey key, Action<TValue> action, bool withBuffer)
        {
            var subscription = ValueAtObservable(key).Subscribe(action);

            if (_activeDictionary[key] != null && withBuffer)
            {
                action.Invoke(_activeDictionary[key]);
            }

            return subscription;
        }
        
        private void RaiseValue(TKey key, TValue value)
        {
            if (_variableEventType == VariableEventType.ValueChange && _activeDictionary[key].Equals(value))
                return;
            
            if (_valueSubjects.TryGetValue(key, out var subject))
            {
                subject.OnNext(value);
            }
        }
        
        private void ClearValueSubscriptions()
        {
            foreach (var subject in _valueSubjects.Values)
            {
                subject.Dispose();
            }
            _valueSubjects.Clear();
        }
        
        private void RemoveValueSubscription(TKey key)
        {
            if (_valueSubjects.TryGetValue(key, out var subject))
            {
                subject.Dispose();
                _valueSubjects.Remove(key);
            }
        }

        #endregion
    }
}

#endif