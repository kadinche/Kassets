#if KASSETS_UNITASK

using System;
using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using Cysharp.Threading.Tasks.Linq;
using UnityEngine;

namespace Kadinche.Kassets.RequestResponseSystem
{
    public abstract partial class RequestResponseEvent<TRequest, TResponse>
    {
        private readonly AsyncReactiveProperty<object> _requestReactiveProperty =
            new AsyncReactiveProperty<object>(default);

        private readonly AsyncReactiveProperty<TResponse> _responseReactiveProperty =
            new AsyncReactiveProperty<TResponse>(default);

        private readonly Queue<TRequest> _asyncRequests = new Queue<TRequest>();

        public UniTask RequestAsync(CancellationToken cancellationToken = default) => RequestAsync(default, cancellationToken);

        public UniTask<TResponse> RequestAsync(TRequest request, CancellationToken cancellationToken = default)
        {
            var token = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken, cts.Token).Token;
            var task = _responseReactiveProperty.WaitAsync(token);
            _asyncRequests.Enqueue(request);
            _requestReactiveProperty.Value = this;
            return task;
        }
        
        public async UniTask ResponseAsync(Func<TRequest, UniTask<TResponse>> responseFunc, CancellationToken cancellationToken = default)
        {
            var token = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken, cts.Token).Token;
            token.ThrowIfCancellationRequested();
            
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
        
        public async UniTask ResponseAsync(Func<TRequest, CancellationToken, UniTask<TResponse>> responseFunc, CancellationToken cancellationToken = default)
        {
            var token = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken, cts.Token).Token;
            if (_requests.Count > 0)
            {
                var request = _requests.Dequeue();
                var response = await responseFunc.Invoke(request.Item1, token);
                request.Item2.Invoke(response);
            }
            else if (_asyncRequests.Count > 0)
            {
                var request = _asyncRequests.Dequeue();
                var response = await responseFunc.Invoke(request, token);
                _responseReactiveProperty.Value = response;
            }
        }

        public IDisposable RegisterResponse(Func<TRequest, UniTask<TResponse>> responseFunc,
            CancellationToken cancellationToken = default,
            bool overrideResponse = true)
        {
            if (!overrideResponse && responseSubscription != null)
            {
                Debug.LogWarning("Responder already exist.");
                return responseSubscription;
            }

            responseSubscription?.Dispose();

            responseSubscription =
                _requestReactiveProperty.SubscribeAwait(async _ => await ResponseAsync(responseFunc, cancellationToken));

            while (_requests.Count > 0)
            {
                _requestReactiveProperty.Value = this;
            }

            return responseSubscription;
        }
        
        public IDisposable RegisterResponse(Func<TRequest, CancellationToken, UniTask<TResponse>> responseFunc,
            CancellationToken cancellationToken = default,
            bool overrideResponse = true)
        {
            if (!overrideResponse && responseSubscription != null)
            {
                Debug.LogWarning("Responder already exist.");
                return responseSubscription;
            }

            responseSubscription?.Dispose();
            responseSubscription =
                _requestReactiveProperty.SubscribeAwait(async _ => await ResponseAsync(responseFunc, cancellationToken));

            while (_requests.Count > 0)
            {
                _requestReactiveProperty.Value = this;
            }

            return responseSubscription;
        }

        public UniTask<TResponse> WaitForResponse(CancellationToken token) => _responseReactiveProperty.WaitAsync(token);
    }
    
#if KASSETS_UNIRX
    public abstract partial class RequestResponseEvent<TRequest, TResponse>
    {
        private void TryRespond_UniTask()
        {
            _requestReactiveProperty.Value = this;
        }
        
        public void Response_UniTask(Func<TRequest, TResponse> responseFunc)
        {
            if (_requests.Count > 0)
            {
                var request = _requests.Dequeue();
                var response = responseFunc.Invoke(request.Item1);
                request.Item2.Invoke(response);
                responseValue = response;
            }
            else if (_asyncRequests.Count > 0)
            {
                var request = _asyncRequests.Dequeue();
                var response = responseFunc.Invoke(request);
                responseValue = response;
                _responseReactiveProperty.Value = response;
            }
        }

        private IDisposable HandleSubscribe_UniTask(Func<TRequest, TResponse> responseFunc)
        {
            return _requestReactiveProperty.Subscribe(_ => Response(responseFunc));
        }
        
        private IDisposable HandleSubscribeToResponse_UniTask(Action action)
        {
            return _responseReactiveProperty.Subscribe(_ => action.Invoke());
        }
        
        private IDisposable HandleSubscribeToResponse_UniTask(Action<TResponse> action)
        {
            return _responseReactiveProperty.Subscribe(action);
        }
        
        private void Dispose_UniTask()
        {
            base.Dispose();
            _requests.Clear();
            responseSubscription?.Dispose();
            _requestReactiveProperty.Dispose();
            _responseReactiveProperty.Dispose();
        }
    }
#else
    public abstract partial class RequestResponseEvent<TRequest, TResponse>
    {
        private void TryRespond()
        {
            _requestReactiveProperty.Value = this;
        }
        
        public void Response(Func<TRequest, TResponse> responseFunc)
        {
            if (_requests.Count > 0)
            {
                var request = _requests.Dequeue();
                var response = responseFunc.Invoke(request.Item1);
                request.Item2.Invoke(response);
                responseValue = response;
            }
            else if (_asyncRequests.Count > 0)
            {
                var request = _asyncRequests.Dequeue();
                var response = responseFunc.Invoke(request);
                responseValue = response;
                _responseReactiveProperty.Value = response;
            }
        }


        private IDisposable HandleSubscribe(Func<TRequest, TResponse> responseFunc)
        {
            return _requestReactiveProperty.Subscribe(_ => Response(responseFunc));
        }
        
        private IDisposable HandleSubscribeToResponse(Action action)
        {
            return _responseReactiveProperty.Subscribe(_ => action.Invoke());
        }
        
        private IDisposable HandleSubscribeToResponse(Action<TResponse> action)
        {
            return _responseReactiveProperty.Subscribe(action);
        }
        
        public override void Dispose()
        {
            base.Dispose();
            _requests.Clear();
            responseSubscription?.Dispose();
            _requestReactiveProperty.Dispose();
            _responseReactiveProperty.Dispose();
        }
    }
#endif
}

#endif