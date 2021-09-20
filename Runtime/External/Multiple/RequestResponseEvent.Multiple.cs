#if KASSETS_MULTI_LIBRARY
using System;

namespace Kadinche.Kassets.RequestResponseSystem
{
    public abstract partial class RequestResponseEvent<TRequest, TResponse>
    {
        private void TryRespond()
        {
            if (_defaultSubscribeBehavior == LibraryEnum.UniRx)
            {
                TryRespond_UniRx();
            }
            else //if (_defaultSubscribeBehavior == LibraryEnum.UniTask)
            {
                TryRespond_UniTask();
            }
        }

        private IDisposable HandleSubscribe(Func<TRequest, TResponse> responseFunc)
        {
            if (_defaultSubscribeBehavior == LibraryEnum.UniRx)
            {
                return HandleSubscribe_UniRx(responseFunc);
            }
            else //if (_defaultSubscribeBehavior == LibraryEnum.UniTask)
            {
                return HandleSubscribe_UniTask(responseFunc);
            }
        }

        public override void Dispose()
        {
            Dispose_UniRx();
            Dispose_UniTask();
        }
    }
    
#if !KASSETS_UNIRX
    public abstract partial class RequestResponseEvent<TRequest, TResponse>
    {
        private void TryRespond_UniRx()
        {
            throw new Exception(ErrMsgUniRx);
        }
        
        private IDisposable HandleSubscribe_UniRx(Func<TRequest, TResponse> responseFunc)
        {
            throw new Exception(ErrMsgUniRx);
        }

        private void Dispose_UniRx()
        {
            throw new Exception(ErrMsgUniRx);
        }
    }
#endif
    
#if !KASSETS_UNITASK
    public abstract partial class RequestResponseEvent<TRequest, TResponse>
    {
        private void TryRespond_UniTask()
        {
            throw new Exception(ErrMsgUniTask);
        }
        
        private IDisposable HandleSubscribe_UniTask(Func<TRequest, TResponse> responseFunc)
        {
            throw new Exception(ErrMsgUniTask);
        }

        private void Dispose_UniTask()
        {
            throw new Exception(ErrMsgUniTask);
        }
    }
#endif
}

#endif