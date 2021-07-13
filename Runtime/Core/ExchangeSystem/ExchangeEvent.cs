using System;
using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Kadinche.Kassets.ExchangeSystem
{
    [CreateAssetMenu(fileName = "ExchangeEvent", menuName = MenuHelper.DefaultExchangeEventMenu + "ExchangeEvent")]
    public class ExchangeEvent : ScriptableObject, ISerializationCallbackReceiver, IDisposable
    {
        private readonly Queue<AsyncReactiveProperty<AsyncUnit>> _requests = new Queue<AsyncReactiveProperty<AsyncUnit>>();
        protected CancellationTokenSource cts = new CancellationTokenSource();

        public UniTask RequestAsync() => RequestAsync(cts.Token);
        public UniTask RequestAsync(CancellationToken cancellationToken)
        {
            var responder = new AsyncReactiveProperty<AsyncUnit>(AsyncUnit.Default);
            _requests.Enqueue(responder);
            return responder.WaitAsync(cancellationToken);
        }
        
        private async UniTaskVoid InternalRequest(Action onResponse)
        {
            await RequestAsync();
            onResponse?.Invoke();
        }
        
        public void Request(Action onResponse) => InternalRequest(onResponse).Forget();

        public void Response(Action response)
        {
            if (_requests.Count == 0) return;
            
            response?.Invoke();
            var responder = _requests.Dequeue();
                responder.Value = AsyncUnit.Default;
                responder.AddTo(cts.Token); // register handled request for disposal
        }
        
        public void Response() => Response(null);

        public UniTask ResponseAsync() => ResponseAsync(null, cts.Token);
        public UniTask ResponseAsync(CancellationToken cancellationToken) => ResponseAsync(null, cancellationToken);
        public async UniTask ResponseAsync(Action response, CancellationToken cancellationToken)
        {
            await UniTask.WaitUntil(() => _requests.Count > 0, cancellationToken: cancellationToken);
            Response(response);
        }

        private async UniTaskVoid SubscribeResponseInternal(Action response, CancellationToken cancellationToken)
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                await ResponseAsync(response, cancellationToken);
            }
        }

        public void SubscribeResponse(Action response, CancellationToken cancellationToken) => 
            SubscribeResponseInternal(response, cancellationToken).Forget();

        public void Cancel(bool reset = true)
        {
            cts?.Cancel();
            cts?.Dispose();
            
            if (reset)
                cts = new CancellationTokenSource();
        }

        public virtual void OnBeforeSerialize() { }
        
        public virtual void OnAfterDeserialize() => Cancel();

        public virtual void Dispose() => Cancel(false);

        private void OnDestroy() => Dispose();
    }

    public abstract class ExchangeEvent<TRequest, TResponse> : ExchangeEvent
    {
        private readonly Queue<Tuple<TRequest, AsyncReactiveProperty<TResponse>>> _requests = new Queue<Tuple<TRequest, AsyncReactiveProperty<TResponse>>>();
        
        private async UniTaskVoid InternalRequest(TRequest requestValue, Action<TResponse> onResponse)
        {
            var responseValue = await RequestAsync(requestValue);
            onResponse?.Invoke(responseValue);
        }
        
        public void Request(TRequest requestValue, Action<TResponse> onResponse) => InternalRequest(requestValue, onResponse).Forget();
        
        public UniTask<TResponse> RequestAsync(TRequest requestValue = default) => RequestAsync(requestValue, cts.Token);
        public UniTask<TResponse> RequestAsync(TRequest requestValue, CancellationToken cancellationToken)
        {
            var responder = new AsyncReactiveProperty<TResponse>(default);
            _requests.Enqueue(new Tuple<TRequest, AsyncReactiveProperty<TResponse>>(requestValue, responder));
            return responder.WaitAsync(cancellationToken);
        }

        public void Response(Func<TRequest, TResponse> responseFunc)
        {
            if (_requests.Count == 0) return;
            
            var request = _requests.Dequeue();
            var requestValue = request.Item1;
            var responder = request.Item2;
            responder.Value = responseFunc.Invoke(requestValue);
            responder.AddTo(cts.Token); // register handled request for disposal
        }

        public UniTask ResponseAsync(Func<TRequest, TResponse> responseFunc) => ResponseAsync(responseFunc, cts.Token);
        public async UniTask ResponseAsync(Func<TRequest, TResponse> responseFunc, CancellationToken cancellationToken)
        {
            await UniTask.WaitUntil(() => _requests.Count > 0, cancellationToken: cancellationToken);
            Response(responseFunc);
        }
        
        private async UniTaskVoid SubscribeResponseInternal(Func<TRequest, TResponse> responseFunc, CancellationToken cancellationToken)
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                await ResponseAsync(responseFunc, cancellationToken);
            }
        }

        public void SubscribeResponse(Func<TRequest, TResponse> responseFunc, CancellationToken cancellationToken) => 
            SubscribeResponseInternal(responseFunc, cancellationToken).Forget();
    }

    public abstract class ExchangeEvent<T> : ExchangeEvent<T, T>
    {
    }
}