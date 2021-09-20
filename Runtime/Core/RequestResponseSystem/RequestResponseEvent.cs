using System;
using System.Collections.Generic;
using Kadinche.Kassets.EventSystem;
using UnityEngine;

namespace Kadinche.Kassets.RequestResponseSystem
{
    public abstract partial class RequestResponseEvent<TRequest, TResponse> : GameEvent<TRequest>
    {
        private readonly Queue<Tuple<TRequest, Action<TResponse>>> _requests =
            new Queue<Tuple<TRequest, Action<TResponse>>>();

        internal IDisposable responseSubscription;

        public void Request(Action onResponse) => Request(default, _ => onResponse.Invoke());

        public void Request(TRequest param, Action<TResponse> onResponse)
        {
            _requests.Enqueue(new Tuple<TRequest, Action<TResponse>>(param, onResponse));
            TryRespond();
        }

        public IDisposable SubscribeResponse(Func<TRequest, TResponse> responseFunc, bool overrideResponse = true)
        {
            if (!overrideResponse && responseSubscription != null)
            {
                Debug.LogWarning("Responder already exist.");
                return responseSubscription;
            }

            responseSubscription?.Dispose();

            responseSubscription ??= HandleSubscribe(responseFunc);

            while (_requests.Count > 0)
            {
                TryRespond();
            }

            return responseSubscription;
        }
    }

    public abstract class RequestResponseEvent<T> : RequestResponseEvent<T, T>
    {
    }

#if !KASSETS_UNITASK
    public abstract partial class RequestResponseEvent<TRequest, TResponse>
    {
        public void Response(Func<TRequest, TResponse> responseFunc)
        {
            if (_requests.Count <= 0) return;

            var request = _requests.Dequeue();
            var response = responseFunc.Invoke(request.Item1);
            request.Item2.Invoke(response);
        }
    }
#endif

#if !KASSETS_UNIRX && !KASSETS_UNITASK
    public abstract partial class RequestResponseEvent<TRequest, TResponse>
    {
        private void TryRespond()
        {
            if (responseSubscription is ResponseSubscription<TRequest, TResponse> subscription)
            {
                Response(subscription.Invoke);
            }
        }

        private IDisposable HandleSubscribe(Func<TRequest, TResponse> responseFunc)
        {
            return new ResponseSubscription<TRequest, TResponse>(this, responseFunc);
        }

        public override void Dispose()
        {
            base.Dispose();
            _requests.Clear();
            responseSubscription?.Dispose();
        }
    }
    
    internal class ResponseSubscription<TRequest, TResponse> : IDisposable
    {
        private RequestResponseEvent<TRequest, TResponse> _source;
        private Func<TRequest, TResponse> _responseFunc;
        
        public ResponseSubscription(
            RequestResponseEvent<TRequest, TResponse> source,
            Func<TRequest, TResponse> responseFunc)
        {
            _source = source;
            _responseFunc = responseFunc;
        }

        public TResponse Invoke(TRequest param) => _responseFunc.Invoke(param);
        
        public void Dispose()
        {
            _responseFunc = null;
            _source.responseSubscription = null;
            _source = null;
        }
    }
#endif
}