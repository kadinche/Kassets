#if KASSETS_UNIRX && !KASSETS_R3
using System;
using UniRx;

namespace Kadinche.Kassets.Transaction
{
    public abstract partial class TransactionCore<TRequest, TResponse> : IObservable<TResponse>
    {
        private readonly Subject<object> _requestSubject = new Subject<object>();
        private readonly Subject<TResponse> _responseSubject = new Subject<TResponse>();
        
        public IDisposable Subscribe(IObserver<TResponse> observer) => _responseSubject.Subscribe(observer);
    }
    
#if KASSETS_UNITASK
    public abstract partial class TransactionCore<TRequest, TResponse>
    {
        private void TryRespond_UniRx()
        {
            _requestSubject.OnNext(this);
        }
        
        public void Response_UniRx(Func<TRequest, TResponse> responseFunc)
        {
            if (_requests.Count <= 0) return;

            var request = _requests.Dequeue();
            var response = responseFunc.Invoke(request.Item1);
            request.Item2.Invoke(response);
            responseValue = response;
            _responseSubject.OnNext(response);
        }
        
        private IDisposable HandleSubscribe_UniRx(Func<TRequest, TResponse> responseFunc)
        {
            return _requestSubject.Subscribe(_ => Response(responseFunc));
        }
        
        private IDisposable HandleSubscribeToResponse_UniRx(Action action)
        {
            return _responseSubject.Subscribe(_ => action.Invoke());
        }
        
        private IDisposable HandleSubscribeToResponse_UniRx(Action<TResponse> action)
        {
            return _responseSubject.Subscribe(action);
        }

        protected override void Dispose_UniRx()
        {
            _requestSubject.Dispose();
            _responseSubject.Dispose();
            base.Dispose_UniRx();
        }
    }
#else
    public abstract partial class TransactionCore<TRequest, TResponse>
    {
        private void TryRespond()
        {
            _requestSubject.OnNext(this);
        }

        private void RaiseResponse(TResponse response)
        {
            responseValue = response;
            _responseSubject.OnNext(response);
        }
        
        private IDisposable HandleSubscribe(Func<TRequest, TResponse> responseFunc)
        {
            return _requestSubject.Subscribe(_ => Response(responseFunc));
        }
        
        private IDisposable HandleSubscribeToResponse(Action action)
        {
            return _responseSubject.Subscribe(_ => action.Invoke());
        }
        
        private IDisposable HandleSubscribeToResponse(Action<TResponse> action)
        {
            return _responseSubject.Subscribe(action);
        }

        public override void Dispose()
        {
            base.Dispose();
            _requests.Clear();
            responseSubscription?.Dispose();
            _requestSubject.Dispose();
            _responseSubject.Dispose();
        }
    }
#endif
}

#endif