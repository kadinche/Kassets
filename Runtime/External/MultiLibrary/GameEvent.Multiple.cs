#if (KASSETS_R3 || KASSETS_UNITASK) && KASSETS_UNITASK

using System;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Kadinche.Kassets.EventSystem
{
    public partial class GameEvent
    {
        [Tooltip("Default Event Subscription behavior. By default, Push-based use UniRx, and Pull-based use UniTask.")]
        [SerializeField] protected SubscribeBehavior defaultSubscribeBehavior;

        /// <summary>
        /// Raise the event.
        /// </summary>
        public virtual void Raise()
        {
            Raise_UniRx();
            Raise_UniTask();
        }

        public IDisposable Subscribe(Action action)
        {
            return defaultSubscribeBehavior switch
            {
                SubscribeBehavior.Push => Subscribe_UniRx(action),
                SubscribeBehavior.Pull => Subscribe_UniTask(action),
                _ => throw new ArgumentOutOfRangeException()
            };
        }

        public override void Dispose()
        {
            Dispose_UniRx();
            Dispose_UniTask();
            base.Dispose();
        }
    }
    
    public abstract partial class GameEvent<T>
    {
        public virtual void Raise(T param)
        {
            Raise_UniRx(param);
            Raise_UniTask(param);
        }

        public IDisposable Subscribe(Action<T> action)
        {
            return defaultSubscribeBehavior switch
            {
                SubscribeBehavior.Push => Subscribe_UniRx(action),
                SubscribeBehavior.Pull => Subscribe_UniTask(action),
                _ => throw new ArgumentOutOfRangeException()
            };
        }
        
        public IUniTaskAsyncEnumerable<T> ToUniTaskAsyncEnumerable() => this;
    }

#if !KASSETS_R3
    public partial class GameEvent
    {
        protected const string ErrMsgR3 = "KASSETS_R3 is undefined. Could you forget to import R3?";
        private void Raise_R3()
        {
            throw new Exception(ErrMsgR3);
        }

        private IDisposable Subscribe_R3(Action action)
        {
            throw new Exception(ErrMsgR3);
        }

        private void Dispose_R3()
        {
            throw new Exception(ErrMsgR3);
        }
    }

    public abstract partial class GameEvent<T>
    {
        private void Raise_R3(T param)
        {
            throw new Exception(ErrMsgR3);
        }

        private IDisposable Subscribe_R3(Action<T> action)
        {
            throw new Exception(ErrMsgR3);
        }
    }
#endif
    
#if !KASSETS_R3 && !KASSETS_UNIRX
    public partial class GameEvent
    {
        protected const string ErrMsgUniRx = "KASSETS_UNIRX is undefined. Could you forget to import UniRx?";
        private void Raise_UniRx()
        {
            throw new Exception(ErrMsgUniRx);
        }

        private IDisposable Subscribe_UniRx(Action action)
        {
            throw new Exception(ErrMsgUniRx);
        }

        private void Dispose_UniRx()
        {
            throw new Exception(ErrMsgUniRx);
        }
    }

    public abstract partial class GameEvent<T>
    {
        private void Raise_UniRx(T param)
        {
            throw new Exception(ErrMsgUniRx);
        }

        private IDisposable Subscribe_UniRx(Action<T> action)
        {
            throw new Exception(ErrMsgUniRx);
        }
    }
#endif
    
#if !KASSETS_UNITASK
    public partial class GameEvent
    {
        protected const string ErrMsgUniTask = "KASSETS_UNITASK is undefined. Could you forget to import UniTask?";
        private void Raise_UniTask()
        {
            throw new Exception(ErrMsgUniTask);
        }

        private IDisposable Subscribe_UniTask(Action action)
        {
            throw new Exception(ErrMsgUniTask);
        }

        private void Dispose_UniTask()
        {
            throw new Exception(ErrMsgUniTask);
        }
    }

    public abstract partial class GameEvent<T>
    {
        private void Raise_UniTask(T param)
        {
            throw new Exception(ErrMsgUniTask);
        }

        private IDisposable Subscribe_UniTask(Action<T> action)
        {
            throw new Exception(ErrMsgUniTask);
        }
    }
#endif
}

#endif