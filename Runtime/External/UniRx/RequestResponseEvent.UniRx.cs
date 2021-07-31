#if KASSETS_UNIRX
using System;
using UniRx;

namespace Kadinche.Kassets.RequestResponseSystem
{
    public abstract partial class RequestResponseEvent<TRequest, TResponse>
    {
        private readonly Subject<object> _requestSubject = new Subject<object>();

        private void TryRespond()
        {
            _requestSubject.OnNext(this);
        }
        
        private IDisposable HandleSubscribe(Func<TRequest, TResponse> responseFunc)
        {
            return _requestSubject.Subscribe(_ => Response(responseFunc));
        }

        public override void Dispose()
        {
            base.Dispose();
            _requests.Clear();
            responseSubscription?.Dispose();
            _requestSubject.Dispose();
        }
    }
}
#endif