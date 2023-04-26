#if KASSETS_UNIRX && KASSETS_UNITASK
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
        
        private void RaiseCount()
        {
            RaiseCount_UniRx();
            RaiseCount_UniTask();
        }
        
        private void RaiseValueAt(int index, T value)
        {
            RaiseValueAt_UniRx(index, value);
            RaiseValueAt_UniTask(index, value);
        }
        
        public IDisposable SubscribeOnAdd(Action<T> action)
        {
            return defaultSubscribeBehavior switch
            {
                SubscribeBehavior.Push => SubscribeOnAdd_UniRx(action),
                SubscribeBehavior.Pull => SubscribeOnAdd_UniTask(action),
                _ => throw new ArgumentOutOfRangeException()
            };
        }

        public IDisposable SubscribeOnRemove(Action<T> action)
        {
            return defaultSubscribeBehavior switch
            {
                SubscribeBehavior.Push => SubscribeOnRemove_UniRx(action),
                SubscribeBehavior.Pull => SubscribeOnRemove_UniTask(action),
                _ => throw new ArgumentOutOfRangeException()
            };
        }

        public IDisposable SubscribeOnClear(Action action)
        {
            return defaultSubscribeBehavior switch
            {
                SubscribeBehavior.Push => SubscribeOnClear_UniRx(action),
                SubscribeBehavior.Pull => SubscribeOnClear_UniTask(action),
                _ => throw new ArgumentOutOfRangeException()
            };
        }
        
        public IDisposable SubscribeToCount(Action<int> action)
        {
            return defaultSubscribeBehavior switch
            {
                SubscribeBehavior.Push => SubscribeToCount_UniRx(action),
                SubscribeBehavior.Pull => SubscribeToCount_UniTask(action),
                _ => throw new ArgumentOutOfRangeException()
            };
        }

        public IDisposable SubscribeToValueAt(int index, Action<T> action)
        {
            return defaultSubscribeBehavior switch
            {
                SubscribeBehavior.Push => SubscribeToValueAt_UniRx(index, action),
                SubscribeBehavior.Pull => SubscribeToValueAt_UniTask(index, action),
                _ => throw new ArgumentOutOfRangeException()
            };
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
            return defaultSubscribeBehavior switch
            {
                SubscribeBehavior.Push => SubscribeToValue_UniRx(key, action),
                SubscribeBehavior.Pull => SubscribeToValue_UniTask(key, action),
                _ => throw new ArgumentOutOfRangeException()
            };
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
    
#if !KASSETS_UNIRX
    public abstract partial class Collection<T>
    {
        private void RaiseOnAdd_UniRx(T addedValue)
        {
            throw new Exception(ErrMsgUniRx);
        }

        private void RaiseOnRemove_UniRx(T removedValue)
        {
            throw new Exception(ErrMsgUniRx);
        }

        private void RaiseOnClear_UniRx()
        {
            throw new Exception(ErrMsgUniRx);
        }
        
        private void RaiseValueAt_UniRx(int index, T value)
        {
            throw new Exception(ErrMsgUniRx);
        }
        
        public IDisposable SubscribeOnAdd_UniRx(Action<T> action)
        {
            throw new Exception(ErrMsgUniRx);
        }

        public IDisposable SubscribeOnRemove_UniRx(Action<T> action)
        {
            throw new Exception(ErrMsgUniRx);
        }

        public IDisposable SubscribeOnClear_UniRx(Action action)
        {
            throw new Exception(ErrMsgUniRx);
        }

        public IDisposable SubscribeToValueAt_UniRx(int index, Action<T> action)
        {
            throw new Exception(ErrMsgUniRx);
        }
        
        private void ClearValueSubscriptions_UniRx()
        {
            throw new Exception(ErrMsgUniRx);
        }
        
        private void RemoveValueSubscription_UniRx(int index)
        {
            throw new Exception(ErrMsgUniRx);
        }

        private void DisposeSubscriptions_UniRx()
        {
            throw new Exception(ErrMsgUniRx);
        }
    }

    public abstract partial class Collection<TKey, TValue>
    {
        private void RaiseValue_UniRx(TKey key, TValue value)
        {
            throw new Exception(ErrMsgUniRx);
        }
        
        public IDisposable SubscribeToValue_UniRx(TKey key, Action<TValue> action)
        {
            throw new Exception(ErrMsgUniRx);
        }
        
        private void ClearValueSubscriptions_UniRx()
        {
            throw new Exception(ErrMsgUniRx);
        }
        
        private void RemoveValueSubscription_UniRx(TKey key)
        {
            throw new Exception(ErrMsgUniRx);
        }
    }
#endif
    
#if !KASSETS_UNITASK
    public abstract partial class Collection<T>
    {
        private void RaiseOnAdd_UniTask(T addedValue)
        {
            throw new Exception(ErrMsgUniTask);
        }

        private void RaiseOnRemove_UniTask(T removedValue)
        {
            throw new Exception(ErrMsgUniTask);
        }

        private void RaiseOnClear_UniTask()
        {
            throw new Exception(ErrMsgUniTask);
        }
        
        private void RaiseValueAt_UniTask(int index, T value)
        {
            throw new Exception(ErrMsgUniTask);
        }
        
        public IDisposable SubscribeOnAdd_UniTask(Action<T> action)
        {
            throw new Exception(ErrMsgUniTask);
        }

        public IDisposable SubscribeOnRemove_UniTask(Action<T> action)
        {
            throw new Exception(ErrMsgUniTask);
        }

        public IDisposable SubscribeOnClear_UniTask(Action action)
        {
            throw new Exception(ErrMsgUniTask);
        }

        public IDisposable SubscribeToValueAt_UniTask(int index, Action<T> action)
        {
            throw new Exception(ErrMsgUniTask);
        }
        
        private void ClearValueSubscriptions_UniTask()
        {
            throw new Exception(ErrMsgUniTask);
        }
        
        private void RemoveValueSubscription_UniTask(int index)
        {
            throw new Exception(ErrMsgUniTask);
        }

        private void DisposeSubscriptions_UniTask()
        {
            throw new Exception(ErrMsgUniTask);
        }
    }

    public abstract partial class Collection<TKey, TValue>
    {
        private void RaiseValue_UniTask(TKey key, TValue value)
        {
            throw new Exception(ErrMsgUniTask);
        }
        
        public IDisposable SubscribeToValue_UniTask(TKey key, Action<TValue> action)
        {
            throw new Exception(ErrMsgUniTask);
        }
        
        private void ClearValueSubscriptions_UniTask()
        {
            throw new Exception(ErrMsgUniTask);
        }
        
        private void RemoveValueSubscription_UniTask(TKey key)
        {
            throw new Exception(ErrMsgUniTask);
        }
    }
#endif
}

#endif