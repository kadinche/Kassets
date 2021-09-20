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
}

#endif