#if KASSETS_UNIRX && KASSETS_UNITASK

using System;
using UnityEngine;

namespace Kadinche.Kassets.EventSystem
{
    public partial class GameEvent
    {
        [Tooltip("Customizable settings for current Kassets instance.")]
        [SerializeField] protected InstanceSettings instanceSettings;

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
            if (instanceSettings.defaultSubscribeBehavior == LibraryEnum.UniRx)
            {
                return Subscribe_UniRx(action);
            }
            else // if (_activeLibrary == LibraryEnum.UniTask)
            {
                return Subscribe_UniTask(action);
            }
        }

        public override void Dispose()
        {
            Dispose_UniRx();
            Dispose_UniTask();
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
            if (instanceSettings.defaultSubscribeBehavior == LibraryEnum.UniRx)
            {
                return Subscribe_UniRx(action);
            }
            else // if (_activeLibrary == LibraryEnum.UniTask)
            {
                return Subscribe_UniTask(action);
            }
        }
    }

#if !KASSETS_UNIRX
    public partial class GameEvent
    {
        protected const string ErrMsgUniRx = "KASSETS_MULTI_LIBRARY was defined, but UniRx not found. Could you forget to import UniRx?";
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
        protected const string ErrMsgUniTask = "KASSETS_MULTI_LIBRARY was defined, but UniTask not found. Could you forget to import UniTask?";
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