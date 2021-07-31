#if KASSETS_UNITASK

using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Cysharp.Threading.Tasks.Linq;
using UnityEngine;

namespace Kadinche.Kassets.RequestResponseSystem
{
    public abstract partial class RequestResponseEvent<TRequest, TResponse>
    {
        private readonly AsyncReactiveProperty<object> _requestReactiveProperty = new AsyncReactiveProperty<object>(default);
        private readonly AsyncReactiveProperty<TResponse> _responseReactiveProperty = new AsyncReactiveProperty<TResponse>(default);
        private readonly Queue<TRequest> _asyncRequests = new Queue<TRequest>();

        public UniTask RequestAsync() => RequestAsync(default);
        
        public UniTask<TResponse> RequestAsync(TRequest request)
        {
            var task = _responseReactiveProperty.WaitAsync(cts.Token);
            _asyncRequests.Enqueue(request);
            TryRespond();
            return task;
        }
        
        public void Response(Func<TRequest, TResponse> responseFunc)
        {
            if (_requests.Count > 0)
            {
                var request = _requests.Dequeue();
                var response = responseFunc.Invoke(request.Item1);
                request.Item2.Invoke(response);
            }
            else if (_asyncRequests.Count > 0)
            {
                var request = _asyncRequests.Dequeue();
                var response = responseFunc.Invoke(request);
                _responseReactiveProperty.Value = response;
            }
        }

        public async UniTask ResponseAsync(Func<TRequest, UniTask<TResponse>> responseFunc)
        {
            if (_requests.Count > 0)
            {
                var request = _requests.Dequeue();
                var response = await responseFunc.Invoke(request.Item1);
                request.Item2.Invoke(response);
            }
            else if (_asyncRequests.Count > 0)
            {
                var request = _asyncRequests.Dequeue();
                var response = await responseFunc.Invoke(request);
                _responseReactiveProperty.Value = response;
            }
        }

        private void TryRespond()
        {
            _requestReactiveProperty.Value = this;
        }
        
        public IDisposable SubscribeResponse(Func<TRequest, UniTask<TResponse>> responseFunc, bool overrideResponse = true)
        {
            if (!overrideResponse && responseSubscription != null)
            {
                Debug.LogWarning("Responder already exist.");
                return responseSubscription;
            }
            
            responseSubscription?.Dispose();

            responseSubscription = _requestReactiveProperty.SubscribeAwait(async _ => await ResponseAsync(responseFunc));

            while (_requests.Count > 0)
            {
                TryRespond();
            }

            return responseSubscription;
        }
        
        private IDisposable HandleSubscribe(Func<TRequest, TResponse> responseFunc)
        {
            return _requestReactiveProperty.Subscribe(_ => Response(responseFunc));
        }
        
        public override void Dispose()
        {
            base.Dispose();
            _requests.Clear();
            responseSubscription?.Dispose();
            _requestReactiveProperty.Dispose();
        }
    }
}

#endif