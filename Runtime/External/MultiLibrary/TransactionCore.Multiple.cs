#if KASSETS_UNIRX && KASSETS_UNITASK
using System;
using Cysharp.Threading.Tasks;

namespace Kadinche.Kassets.Transaction
{
    public abstract partial class TransactionCore<TRequest, TResponse>
    {
        private void TryRespond()
        {
            switch (defaultSubscribeBehavior)
            {
                case SubscribeBehavior.Push:
                    TryRespond_UniRx();
                    break;
                case SubscribeBehavior.Pull:
                    TryRespond_UniTask();
                    break;
            }
        }

        public void Response(Func<TRequest, TResponse> responseFunc)
        {
            switch (defaultSubscribeBehavior)
            {
                case SubscribeBehavior.Push:
                    Response_UniRx(responseFunc);
                    break;
                case SubscribeBehavior.Pull:
                    Response_UniTask(responseFunc);
                    break;
            }
        }

        private IDisposable HandleSubscribe(Func<TRequest, TResponse> responseFunc)
        {
            return defaultSubscribeBehavior switch
            {
                SubscribeBehavior.Push => HandleSubscribe_UniRx(responseFunc),
                SubscribeBehavior.Pull => HandleSubscribe_UniTask(responseFunc),
                _ => throw new ArgumentOutOfRangeException()
            };
        }
        
        private IDisposable HandleSubscribeToResponse(Action action)
        {
            return defaultSubscribeBehavior switch
            {
                SubscribeBehavior.Push => HandleSubscribeToResponse_UniRx(action),
                SubscribeBehavior.Pull => HandleSubscribeToResponse_UniTask(action),
                _ => throw new ArgumentOutOfRangeException()
            };
        }
        
        private IDisposable HandleSubscribeToResponse(Action<TResponse> action)
        {
            return defaultSubscribeBehavior switch
            {
                SubscribeBehavior.Push => HandleSubscribeToResponse_UniRx(action),
                SubscribeBehavior.Pull => HandleSubscribeToResponse_UniTask(action),
                _ => throw new ArgumentOutOfRangeException()
            };
        }

        public IObservable<TRequest> RequestAsObservable() => this;
        public IObservable<TResponse> ResponseAsObservable() => this;
        public IUniTaskAsyncEnumerable<TRequest> RequestAsAsyncEnumerable() => this;
        public IUniTaskAsyncEnumerable<TResponse> ResponseAsAsyncEnumerable() => this;

        public override void Dispose()
        {
            Dispose_UniRx();
            Dispose_UniTask();
            _requests.Clear();
            responseSubscription?.Dispose();
            base.Dispose();
        }
    }
    
#if !KASSETS_UNIRX
    public abstract partial class TransactionCore<TRequest, TResponse>
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
    public abstract partial class TransactionCore<TRequest, TResponse>
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