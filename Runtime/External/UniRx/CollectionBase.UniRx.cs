#if KASSETS_UNIRX
using System;
using System.Collections.Generic;
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

        #endregion
    }
    
#if KASSETS_UNITASK
    public abstract partial class Collection<T>
    {
        private void RaiseOnAdd_UniRx(T addedValue) => _onAddSubject.OnNext(addedValue);
        private void RaiseOnRemove_UniRx(T removedValue) => _onRemoveSubject.OnNext(removedValue);
        private void RaiseOnClear_UniRx() => _onClearSubject.OnNext(this);
        private void RaiseValueAt_UniRx(int index, T value)
        {
            if (_variableEventType == VariableEventType.ValueChange && _value[index].Equals(value))
                return;

            if (_valueSubjects.TryGetValue(index, out var subject))
            {
                subject.OnNext(value);
            }
        }
        
        public IDisposable SubscribeOnAdd_UniRx(Action<T> action)
        {
            return _onAddSubject.Subscribe(action);
        }

        public IDisposable SubscribeOnRemove_UniRx(Action<T> action)
        {
            return _onRemoveSubject.Subscribe(action);
        }

        public IDisposable SubscribeOnClear_UniRx(Action action)
        {
            return _onClearSubject.Subscribe(_ => action.Invoke());
        }

        public IDisposable SubscribeToValueAt_UniRx(int index, Action<T> action)
        {
            return ValueAtObservable(index).Subscribe(action);
        }
        
        private void ClearValueSubscriptions_UniRx()
        {
            foreach (var subject in _valueSubjects.Values)
            {
                subject.Dispose();
            }
            _valueSubjects.Clear();
        }
        
        private void RemoveValueSubscription_UniRx(int index)
        {
            if (_valueSubjects.TryGetValue(index, out var subject))
            {
                subject.Dispose();
                _valueSubjects.Remove(index);
            }
        }

        private void DisposeSubscriptions_UniRx()
        {
            _onAddSubject.Dispose();
            _onRemoveSubject.Dispose();
            _onClearSubject.Dispose();
            ClearValueSubscriptions_UniRx();
        }
    }

    public abstract partial class Collection<TKey, TValue>
    {
        private void RaiseValue_UniRx(TKey key, TValue value)
        {
            if (_variableEventType == VariableEventType.ValueChange && _activeDictionary[key].Equals(value))
                return;
            
            if (_valueSubjects.TryGetValue(key, out var subject))
            {
                subject.OnNext(value);
            }
        }
        
        public IDisposable SubscribeToValue_UniRx(TKey key, Action<TValue> action)
        {
            return ValueAtObservable(key).Subscribe(action);
        }
        
        private void ClearValueSubscriptions_UniRx()
        {
            foreach (var subject in _valueSubjects.Values)
            {
                subject.Dispose();
            }
            _valueSubjects.Clear();
        }
        
        private void RemoveValueSubscription_UniRx(TKey key)
        {
            if (_valueSubjects.TryGetValue(key, out var subject))
            {
                subject.Dispose();
                _valueSubjects.Remove(key);
            }
        }
    }
#else
    public abstract partial class Collection<T>
    {
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

        public IDisposable SubscribeOnAdd(Action<T> action)
        {
            return _onAddSubject.Subscribe(action);
        }

        public IDisposable SubscribeOnRemove(Action<T> action)
        {
            return _onRemoveSubject.Subscribe(action);
        }

        public IDisposable SubscribeOnClear(Action action)
        {
            return _onClearSubject.Subscribe(_ => action.Invoke());
        }

        public IDisposable SubscribeToValueAt(int index, Action<T> action)
        {
            return ValueAtObservable(index).Subscribe(action);
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
    }
    
    public abstract partial class Collection<TKey, TValue>
    {
        private void RaiseValue(TKey key, TValue value)
        {
            if (_variableEventType == VariableEventType.ValueChange && _activeDictionary[key].Equals(value))
                return;
            
            if (_valueSubjects.TryGetValue(key, out var subject))
            {
                subject.OnNext(value);
            }
        }

        public IDisposable SubscribeToValue(TKey key, Action<TValue> action)
        {
            return ValueAtObservable(key).Subscribe(action);
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
    }
#endif
}

#endif