#if KASSETS_MULTI_LIBRARY
using System;

namespace Kadinche.Kassets.Collection
{
    public abstract partial class Collection<T>
    {
        private void RaiseOnAdd(T addedValue)
        {
            RaiseOnAdd_UniRx(addedValue);
            RaiseOnAdd_UniTask(addedValue);
        }

        private void RaiseOnRemove(T removedValue)
        {
            RaiseOnRemove_UniRx(removedValue);
            RaiseOnRemove_UniTask(removedValue);
        }

        private void RaiseOnClear()
        {
            RaiseOnClear_UniRx();
            RaiseOnClear_UniTask();   
        }
        
        private void RaiseValueAt(int index, T value)
        {
            RaiseValueAt_UniRx(index, value);
            RaiseValueAt_UniTask(index, value);
        }
        
        public IDisposable SubscribeOnAdd(Action<T> action)
        {
            if (_defaultSubscribeBehavior == LibraryEnum.UniRx)
            {
                return SubscribeOnAdd_UniRx(action);
            }
            else // if (_activeLibrary == LibraryEnum.UniTask)
            {
                return SubscribeOnAdd_UniTask(action);
            }
        }

        public IDisposable SubscribeOnRemove(Action<T> action)
        {
            if (_defaultSubscribeBehavior == LibraryEnum.UniRx)
            {
                return SubscribeOnRemove_UniRx(action);
            }
            else // if (_activeLibrary == LibraryEnum.UniTask)
            {
                return SubscribeOnRemove_UniTask(action);
            }
        }

        public IDisposable SubscribeOnClear(Action action)
        {
            if (_defaultSubscribeBehavior == LibraryEnum.UniRx)
            {
                return SubscribeOnClear_UniRx(action);
            }
            else // if (_activeLibrary == LibraryEnum.UniTask)
            {
                return SubscribeOnClear_UniTask(action);
            }
        }

        public IDisposable SubscribeToValueAt(int index, Action<T> action)
        {
            if (_defaultSubscribeBehavior == LibraryEnum.UniRx)
            {
                return SubscribeToValueAt_UniRx(index, action);
            }
            else // if (_activeLibrary == LibraryEnum.UniTask)
            {
                return SubscribeToValueAt_UniTask(index, action);
            }
        }
        
        private void ClearValueSubscriptions()
        {
            ClearValueSubscriptions_UniRx();
            ClearValueSubscriptions_UniTask();
        }
        
        private void RemoveValueSubscription(int index)
        {
            RemoveValueSubscription_UniRx(index);
            RemoveValueSubscription_UniTask(index);
        }

        private void DisposeSubscriptions()
        {
            DisposeSubscriptions_UniRx();
            DisposeSubscriptions_UniTask();
        }
    }
    
    public abstract partial class Collection<TKey, TValue>
    {
        private void RaiseValue(TKey key, TValue value)
        {
            RaiseValue_UniRx(key, value);
            RaiseValue_UniTask(key, value);
        }
        
        public IDisposable SubscribeToValue(TKey key, Action<TValue> action)
        {
            if (_defaultSubscribeBehavior == LibraryEnum.UniRx)
            {
                return SubscribeToValue_UniRx(key, action);
            }
            else // if (_activeLibrary == LibraryEnum.UniTask)
            {
                return SubscribeToValue_UniTask(key, action);
            }
        }
        
        private void ClearValueSubscriptions()
        {
            ClearValueSubscriptions_UniRx();
            ClearValueSubscriptions_UniTask();
        }
        
        private void RemoveValueSubscription(TKey key)
        {
            RemoveValueSubscription_UniRx(key);
            RemoveValueSubscription_UniTask(key);
        }
    }
}

#endif